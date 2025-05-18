using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Modifier
{
    readonly List<ICondition> _conds;
    readonly List<IEffect> _effects;
    Card _owner;

    public Modifier(IEnumerable<ICondition> cs, IEnumerable<IEffect> es)
    {
        _conds = cs.ToList();
        _effects = es.ToList();
    }
    public void Bind(Card card)
    {
        _owner = card;
        foreach (var cond in _conds)
            foreach (var evt in cond.SubscribedEvents)
                EventBus.Subscribe(evt, Handle);
    }
    public void Unbind()
    {
        foreach (var cond in _conds)
            foreach (var evt in cond.SubscribedEvents)
                EventBus.Unsubscribe(evt, Handle);
    }
    void Handle(GameEvent e)
    {
        if (_conds.All(cd => cd.Evaluate(_owner, e)))
            foreach (var eff in _effects) eff.Apply(_owner);
    }
}