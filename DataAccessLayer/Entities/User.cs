using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User
    {     
     public int Id { get; set; }

    //Here column type must be changed to varchar and maxlength is set to 100 because in order to make UserName column unique we need to perform this and run the sql command separetly else error in migration occurs. Look at 202402291102387_AddUniqueConstraintToUserNameColumnAfterUpdate migration file to know how this is performed.
    [Column(TypeName = "varchar")]
    [MaxLength(100)]
    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public bool IsAcceptedTerms { get; set; }
}
}
