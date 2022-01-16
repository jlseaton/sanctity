namespace Game.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        public string Pwd { get; set; }

        public int Role { get; set; }

        public bool? Enabled { get; set; }

        public DateTime? Signup { get; set; }

        public DateTime? LastActive { get; set; }
    }
}
