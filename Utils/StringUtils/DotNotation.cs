﻿namespace Abd.Shared.Utils.StringUtils;

public static class DotNotation
{
    public static string ToDotNotation(this string val)
        => val.IsNoE() ? string.Empty : 
            string.Concat(val!.Select(x => char.IsUpper(x) ? "." + x : x.ToString()));
}