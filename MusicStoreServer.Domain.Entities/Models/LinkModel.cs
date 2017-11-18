﻿using MusicStoreServer.Domain.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Models
{
    public class LinkModel : BaseEntity
    {
        [Required]
        public ApplicationUser Owner { get; set; }
        [Required]
        public string MimeType { get; set; }
    }
}