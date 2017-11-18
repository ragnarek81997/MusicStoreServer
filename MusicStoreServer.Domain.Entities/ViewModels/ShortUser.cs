using System;
using System.Collections.Generic;

namespace MusicStoreServer.Domain.Entities.ViewModels
{
    public class ShortUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoId { get; set; }
    }
}