﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Infrastructure
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public string Id { get; set; }
    }
}
