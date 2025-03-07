﻿using CardActionsApi.Models;

namespace CardActionsApi.Specifications.Rules.Enums;

public class AnyOfStatuses : IsSatisfiableEnum<CardStatus, CardDetails> {
    public AnyOfStatuses(CardStatus[] items) : base(items)
    {
    }

    public override bool IsSatisfiedBy(CardDetails cardDetails)
    {
        return _items.Contains(cardDetails.CardStatus);
    }
}