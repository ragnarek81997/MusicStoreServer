﻿using MusicStoreServer.Domain.Entities.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStoreServer.Domain.Entities.Models.Song
{
    public class GenresInSongModel : BaseEntity
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [ForeignKey("SongModel")]
        public string SongId { get; set; }
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [ForeignKey("GenreModel")]
        public string GenreId { get; set; }
    }
}
