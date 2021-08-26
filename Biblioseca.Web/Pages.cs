using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioseca.Web
{
    public static class Pages
    {
        public static class Author
        {
            public const string List = "~/Author/List.aspx";
            public const string Create = "~/Author/Create.aspx";
            public const string Edit = "~/Author/Edit.aspx?id={0}";
        }
        public static class Partner
        {
            public const string List = "~/Partner/Partners.aspx";
            public const string Create = "~/Partner/Create.aspx";
            public const string Edit = "~/Partner/Edit.aspx?id={0}";
        }

        public static class Category
        {
            public const string List = "~/Category/Categorys.aspx";
            public const string Create = "~/Category/Create.aspx";
            
        }

        public static class Books
        {
            public const string List = "~/Book/AvailableBooks.aspx";
            public const string Create = "~/Book/Create.aspx";
            public const string Edit = "~/Book/Edit.aspx?id={0}";
        }

        public static class Loans
        {
            public const string List = "~/Loan/Loans.aspx";
            public const string Create = "~/Loan/Create.aspx";
            public const string Returned = "~/Loan/Returned.aspx";
            public const string Edit = "~/Loan/Edit.aspx?id={0}";
            public const string Details = "~/Loan/Details.aspx?id={0}";
            public const string Delete = "~/Loan/Delete.aspx?id={0}";
            public const string Congrats = "~/Loan/Congrats.aspx";
            
        }

        public static class Error
        {
            public const string BusinessError = "~/BusinessError.aspx?error={0}";
        }
    }
}