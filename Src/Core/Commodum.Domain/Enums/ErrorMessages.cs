using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Commodum.Domain.Enums
{
    public enum ErrorMessages
    {
        [Display(Name = "Username or password invalid.")]
        InvalidCredentials = 500,
        [Display(Name = "Something went wrong.")]
        SomethingWentWrong = 501,
        [Display(Name = "User has not been placed in the tree.")]
        UserPlacementFailed = 502,
        [Display(Name = "User has not been created.")]
        UserNotExist = 503,
        [Display(Name = "User already exist.")]
        UserAlreadyExist = 504,
        [Display(Name = "Failed to update Product.")]
        ProductUpdatedFailed = 505,
        [Display(Name = "Failed to update profile.")]
        ProfileUpdatedFailed = 506,
        [Display(Name = "Username already exist.")]
        UserNameExist = 507,
        [Display(Name = "Failed to create City.")]
        CityCreatedFailed = 508,
        [Display(Name = "Failed to update City.")]
        CityUpdatedFailed = 509,
        [Display(Name = "Failed to create District.")]
        DistrictCreatedFailed = 510,
        [Display(Name = "Failed to update District.")]
        DistrictUpdatedFailed = 511,
        [Display(Name = "Failed to create stakeholder.")]
        StakeHolderCreatedFailed = 512,
        [Display(Name = "Failed to update Password.")]
        PasswordUpdateFailed = 513,
        [Display(Name = "Failed to Create Customer.")]
        CustomerCreatedFailed = 514,
        [Display(Name = "Failed to update Customer Payment.")]
        CustomerPaymentFailed = 515,
        [Display(Name = "Failed to buy Product for Customer.")]
        CustomerProductFailed = 516,
        [Display(Name = "Country manager already exists.")]
        CountryManagerAlreadyExist = 517,
        [Display(Name = "District manager already exists.")]
        DistrictManagerAlreadyExist = 518,
        [Display(Name = "Country already exists.")]
        CountryAlreadyExist = 519,
        [Display(Name = "District already exists.")]
        DistrictAlreadyExist = 520,
        [Display(Name = "City already exists.")]
        CityAlreadyExist = 521,
        [Display(Name = "Customer does not exist.")]
        CustomerDoesnotExist = 522,
        [Display(Name = "Current password is not correct")]
        CurrentPasswordValidation = 523,
        [Display(Name = "Product creation failed for user")]
        UserProductFailed = 524,
        [Display(Name = "Product purchase failed")]
        UserProductPurchaseFailed = 525

        
    }
}
