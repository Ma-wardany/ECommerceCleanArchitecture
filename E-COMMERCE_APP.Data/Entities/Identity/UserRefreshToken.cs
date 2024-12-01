using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace E_COMMERCE_APP.Data.Entities.Identity
{
    public class UserRefreshToken
    {
        [Key]
        public int             Id               { get; set; }
        public string          AccessToken      { get; set; }
        public string          RefreshToken     { get; set; }
        public bool            IsRevoked        { get; set; }
        public DateTime        ExpiresOn        { get; set; }
        public string          UserId           { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty(nameof(ApplicationUser.UserRefreshTokens))]
        public ApplicationUser applicationUser  { get; set; }

    }
}
