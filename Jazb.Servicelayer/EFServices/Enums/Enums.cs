namespace Jazb.Servicelayer.EFServices.Enums
{

    public enum ValentearStatuse
    {
        Win=3,
        Reject=2,
        Aceept=1,
        Registerd=0
    }
    public enum VerifyUserStatus
    {
        VerifiedSuccessfully,
        UserIsbaned,
        VerifiedFaild
    }

    public enum AddUserStatus
    {
        UserNameExist,
        EmailExist,
        AddingUserSuccessfully,
    }

    public enum EditedUserStatus
    {
        UserNameExist,
        EmailExist,
        UserNameOrEmailExist,
        UpdatingUserSuccessfully
    }

    public enum ChangePasswordResult
    {
        ChangedSuccessfully,
        ChangedFaild
    }

    public enum Order
    {
        Asscending,
        Descending
    }

    public enum PostOrderBy
    {
        ById,
        ByTitle,
        ByPostAuthor,
        ByVisitedNumber,
        ByLabels
    }

    public enum UserOrderBy
    {
        UserName,
        PostCount,
        CommentCount,
        RegisterDate,
        IsBaned,
        LoginDate,
        Ip
    }

  

    public enum UserSearchBy
    {
        UserName,
        FirstName,
        LastName,
        Email,
        Ip,
        RoleDescription
    }

    public enum LabelSearchBy
    {
        Name,
        Description
    }

    public enum LabelOrderBy
    {
        Id,
        Name,
        Description,
        PostCount
    }

    public enum UpdatePostStatus
    {
        Successfull,
        Faild
    }

    public enum CommentOrderBy
    {
        AddDate,
        IsApproved,
        LikeCount,
        UserName,
        Author,
        Ip
    }

    public enum CommentSearchBy
    {
        UserName,
        Author,
        Body,
        Ip
    }

    public enum PageSearchBy
    {
        Title,
        UserName,
        ParentTitle
    }

    public enum PageOrderBy
    {
        Title,
        Date,
        CommentCount,
        Status,
        UserName,
        ParentTitle
    }

    public enum CategorySearchBy
    {
        Name,
        Description
    }

    public enum CategoryOrderBy
    {
        Id,
        Name
    }

    public enum ArticleSearchBy
    {
        Title,
        Body,
        Date,
        Category,
        UserName,
    }

    public enum ArticleOrderBy
    {
        Title,
        Date,
        CommentCount,
        Status,
        UserName,
        Order
    }

    public enum DegreeSearchBy
    {
        Title,
        Code,
    }

    public enum DegreeOrderBy
    {
        Title,
        Code
    }



    public enum AzmoonOrderBy
    {
        Active,
        BoxTitle,
        Title,
        ValentearCount,
        AcceptCount,
        RejectCount,
        WinCount,
        StartDate,
        EndDate,
        ID

    }

    public enum AzmoonSearchBy
    {
        BoxTitle,
        Title,
        StartDate,
        EndDate,
        Active,
        NotActive

    }

    public enum ValentearSearchBy
    {
        Melicode,
        FirstName,
        LastName,
        FatherName,
        BirthCertificateNo,
        City,
        Job,
         Place,
        Accept

    }


    public enum AddAzmoonStatus
    {
        Faild,
        AddingAzmoonSuccessfully,
    }



    public enum ConscriptStatusOrderBy
    {
       Title,
       Code,
    }
    public enum ConscriptStatusSearchBy
    {
        Title,
        Code,
    }


    public enum DevotionTypeOrderBy
    {
        Title,
        Grade,
    }
    public enum DevotionTypeSearchBy
    {
        Title,
        Grade,
    }


    public enum EducationSkillOrderBy
    {
        Title,
        Code,
    }
    public enum EducationSkillSearchBy
    {
        Title,
        Code,
    }


    public enum FaithOrderBy
    {
        Title,
        Code,
    }
    public enum FaithSearchBy
    {
        Title,
        Code,
    }

    public enum GenderOrderBy
    {
        Title,
        Code,
    }
    public enum GenderSearchBy
    {
        Title,
        Code,
    }

    public enum JobOrderBy
    {
        Title,
        Code,
    }
    public enum JobSearchBy
    {
        Title,
        Code,
    }

    public enum MarriageStatusOrderBy
    {
        Title,
        Code,
    }
    public enum MarriageStatusSearchBy
    {
        Title,
        Code,
    }
    public enum QoutaTypeOrderBy
    {
        Title,
        Grade,
    }
    public enum QoutaTypeSearchBy
    {
        Title,
        Grade,
    }


    public enum ReligionOrderBy
    {
        Title,
        Code,
    }
    public enum ReligionSearchBy
    {
        Title,
        Code,
    }

    public enum PlaceOrderBy
    {
        Title,
        Code,
    }
    public enum PlaceSearchBy
    {
        Title,
        Code,
    }

}

