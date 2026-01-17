using System.ComponentModel.DataAnnotations;

namespace CitiesManager.Models
{
    public class Employee_Full_Details
    {
        [Key]
        public int? Id { get; set; }
        public long empId { get; set; }
        public string Name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string email_Id { get; set; }
        public string contact_NO { get; set; }
        public string? address { get; set; }
        public string? pincode { get; set; }

    }
}
