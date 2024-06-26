﻿using FluentValidation;

namespace Totvs.Ats.Domain.Common.Validators;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, TProperty> WithAutomaticErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, AbstractValidator<T> validator)
    {
        var memberTypeName = typeof(T).Name;

        var validationRule = validator.LastOrDefault();

        if (validationRule is null)
            return ruleBuilder.WithErrorCode($"{memberTypeName}");

        var propertyName = validationRule.PropertyName;

        var ruleComponent = validationRule.Components.LastOrDefault();

        if (ruleComponent is null)
            return ruleBuilder.WithErrorCode($"{memberTypeName}_{propertyName}");

        var validationType = ruleComponent.Validator.Name.Replace("Validator", "");

        return ruleBuilder.WithErrorCode($"{memberTypeName}_{propertyName}_{validationType}");
    }

    public static IRuleBuilderOptions<T, TProperty> WithHint<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, string hint)
    {
        return ruleBuilder.WithState(t => new Hint(hint));
    }
}
