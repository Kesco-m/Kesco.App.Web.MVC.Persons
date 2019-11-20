namespace Kesco.Web.Mvc.UI.Grid
{
    using System;

    public enum SearchOperation
    {
        IsEqualTo,
        IsNotEqualTo,
        IsLessThan,
        IsLessOrEqualTo,
        IsGreaterThan,
        IsGreaterOrEqualTo,
        IsIn,
        IsNotIn,
        BeginsWith,
        DoesNotBeginWith,
        EndsWith,
        DoesNotEndWith,
        Contains,
        DoesNotContain
    }

	[Flags]
	public enum SearchOperations
	{
		IsEqualTo = 1,
		IsNotEqualTo = 2,
		IsLessThan = 4,
		IsLessOrEqualTo = 8,
		IsGreaterThan = 16,
		IsGreaterOrEqualTo = 32,
		IsIn = 64,
		IsNotIn = 256,
		BeginsWith = 512,
		DoesNotBeginWith = 1024,
		EndsWith = 2048,
		DoesNotEndWith = 4096,
		Contains = 8192,
		DoesNotContain = 16384,
		All = IsEqualTo | IsNotEqualTo | IsLessThan | IsLessOrEqualTo | IsGreaterThan | IsGreaterOrEqualTo | IsIn | IsNotIn | BeginsWith | DoesNotBeginWith | EndsWith | DoesNotEndWith | Contains | DoesNotContain,
		ForStrings = BeginsWith | DoesNotBeginWith | EndsWith | DoesNotEndWith | Contains | DoesNotContain,
		ForNumerics = IsEqualTo | IsNotEqualTo | IsLessThan | IsLessOrEqualTo | IsGreaterThan | IsGreaterOrEqualTo,
		ForDates = IsEqualTo | IsNotEqualTo | IsLessThan | IsLessOrEqualTo | IsGreaterThan | IsGreaterOrEqualTo 
	}
}
