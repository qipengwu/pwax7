using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using pwax7mobileappService.DataObjects;
using pwax7mobileappService.Models;

using AuthenticationUtility;
using Microsoft.OData.Client;
using ODataUtility.Microsoft.Dynamics.DataEntities;

namespace pwax7mobileappService.Controllers
{
    [Authorize]
    public class TodoItemController : TableController<TodoItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            pwax7mobileappContext context = new pwax7mobileappContext();
            DomainManager = new EntityDomainManager<TodoItem>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<TodoItem> GetAllTodoItems()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TodoItem> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TodoItem> PatchTodoItem(string id, Delta<TodoItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(TodoItem item)
        {
            TodoItem current = await InsertAsync(item);
            
            // Begin insert into AX7
            string ODataEntityPath = ClientConfiguration.Default.UriString + "data";

            Uri oDataUri = new Uri(ODataEntityPath, UriKind.Absolute);
            /*
            var ax7context = new Resources(oDataUri);

            
            ax7context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(delegate (object sender, SendingRequest2EventArgs e)
            {
                var authenticationHeader = OAuthHelper.GetAuthenticationHeader();
                e.RequestMessage.SetHeader(OAuthHelper.OAuthHeader, authenticationHeader);
            });
            
            FleetCustomer fleetCustomer = FleetCustomer.CreateFleetCustomer(item.Id, item.Text, 11111, "Wu");

            try
            {
                ax7context.AddToFleetCustomers(fleetCustomer);

                // Send updates to the data service.
                DataServiceResponse response = ax7context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    "An error occurred when saving changes.", ex);
            }
            */
            // End insert into AX7

            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}