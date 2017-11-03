﻿using System;
using System.Collections.Generic;

namespace MusicStoreServer.Domain.Entities.ViewModels
{
    public class ShortUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
    }
}