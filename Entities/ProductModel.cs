
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    public class ProductModel
    {      
        public int Id { get; set; }

        [MaxLength(50)]
        [TrimArrribute]
        [Required(ErrorMessage = "Required field")]
        
        public string Title { get; set; }

        [MaxLength(500)]
        [TrimArrribute]
        [Required(ErrorMessage = "Required field")]
        public string ShortDescription { get; set; }

        [MaxLength(4000)]
        [TrimArrribute]
        [Required(ErrorMessage = "Required field")]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "smalldatetime")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Price { get; set; }

        public DateTime LastModified { get; set; }

        [NotMapped]
        public IFormFile Img1File { get; set; }

        [NotMapped]
        public IFormFile Img2File { get; set; }

        [NotMapped]
        public IFormFile Img3File { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public State State { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
     
        public virtual UserModel Owner { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual UserModel User { get; set; }


    }
}
