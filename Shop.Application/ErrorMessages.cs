using System;
using System.Linq.Expressions;

namespace Shop.Application;

public static class ErrorMessages
{
    public static string NotFound<TSource, TMember>(Expression<Func<TSource, TMember>> memberAccess, TMember memberValue)
    {
        var memberName = ((MemberExpression)memberAccess.Body).Member.Name;
        var message = $"{typeof(TSource).Name} {{ {memberName}: {memberValue} }} does not exist.";

        return message;
    }

    public static string InternalServerError => "Internal Server Error";
}