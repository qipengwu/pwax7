using Microsoft.Azure.Mobile.Server;

namespace pwax7mobileappService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}