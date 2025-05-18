using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Modifier
{
    readonly List<ICondition> _conds;
    readonly List<IEffect> _effects;
    public Card _owner;

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
        {
            // 尝试取第一个 IValueCondition 的数值
            int val = 0;
            var vc = _conds.OfType<IValueCondition>().FirstOrDefault();
            if (vc != null) val = vc.Value;

            foreach (var fx in _effects)
                fx.Apply(_owner, e.Context, val);
        }
    }
}