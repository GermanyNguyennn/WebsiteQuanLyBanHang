using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteQuanLyBanHang.Models.EF
{
    [Table("Table_Contact")]
    public class Contact : CommonAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(500)]
        public string Alias { get; set; }
        [Required(ErrorMessage = "Tên Không Được Để Trống!")]
        [StringLength(50, ErrorMessage = "Tên Không Được Vượt Quá 50 Kí Tự!")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "Email Không Được Vượt Quá 500 Kí Tự!")]
        public string Email { get; set; }
        [StringLength(500, ErrorMessage = "Website Không Được Vượt Quá 500 Kí Tự!")]
        public string Website { get; set; }
        [StringLength(500, ErrorMessage = "Message Không Được Vượt Quá 500 Kí Tự!")]
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}