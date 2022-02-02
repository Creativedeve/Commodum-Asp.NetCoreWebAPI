using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Commodum.Domain.Enums
{
    public enum SuccessMessages
    {
        [Display(Name = "Welcome to admin.")]
        WelcomeAdmin = 100,
        [Display(Name = "User has been placed in the tree successfully.")]
        UserPlaced = 101,
        [Display(Name = "Country has been updated successfully.")]
        CountryUpdated = 102,
        [Display(Name = "Product has been created successfully.")]
        ProductCreated = 103,
        [Display(Name = "Product has been updated successfully.")]
        ProductUpdated = 104,
        [Display(Name = "Profile has been updated successfully.")]
        ProfileUpdated = 105,
        [Display(Name = "City has been created successfully.")]
        CityCreated = 106,
        [Display(Name = "City has been updated successfully.")]
        CityUpdated = 107,
        [Display(Name = "District has been created successfully.")]
        DistrictCreated = 108,
        [Display(Name = "District has been updated successfully.")]
        DistrictUpdated = 109,
        [Display(Name = "StakeHolder has been created successfully.")]
        StakeholderCreated = 110,
        [Display(Name = "Password has been updated successfully.")]
        PasswordUpdated = 111,
        [Display(Name = "Customer has been created successfully.")]
        CustomerCreated = 112,
        [Display(Name = "Payment request has been generated successfully.")]
        CustomerPaymentUpdated = 113,
        [Display(Name = "Product has been sold successfully.")]
        CustomerProductCreated = 114,
        [Display(Name = "Please check your email for the reset password link.")]
        Emailsend = 115,
        [Display(Name = "Valid Customer")]
        CustomerValidCheck = 116,
        [Display(Name = "Product created for customer")]
        UserProductCreated = 117,
        [Display(Name = "Product purchased successfully")]
        UserProductPurchased = 118,
    }
}
