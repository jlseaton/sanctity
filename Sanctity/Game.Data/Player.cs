namespace Game.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Player")]
    public partial class Player
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ServerId { get; set; }

        public int ZoneId { get; set; }

        public int ItemId { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        public bool? Gender { get; set; }

        public int Race { get; set; }

        public int Size { get; set; }

        public int? Speed { get; set; }

        public int Class { get; set; }

        public int Level { get; set; }

        public int Experience { get; set; }

        public int? Alignment { get; set; }

        public int? Diety { get; set; }

        public int? Strength { get; set; }

        public int? Dexterity { get; set; }

        public int? Constitution { get; set; }

        public int? Intelligence { get; set; }

        public int? Wisdom { get; set; }

        public int? Luck { get; set; }

        public int? HitPoints { get; set; }

        public int? MaxHitPoints { get; set; }

        public int? ManaPoints { get; set; }

        public int? MaxManaPoints { get; set; }

        public int? Gold { get; set; }

        public int? Silver { get; set; }

        public int? Copper { get; set; }

        [StringLength(100)]
        public string Bio { get; set; }

        public DateTime? LastActive { get; set; }
    }
}
