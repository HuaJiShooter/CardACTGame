using System;
using System.Collections.Generic;

// ---------------- Event ϵͳ ----------------
public enum GameEvt { HandChanged, CardPlayed, BuffApplied, ManaChanged }

public struct GameEvent {
    public GameEvt Type;
    public object  Source;      // �����߻���
    public object  Target;      // ��Ӱ��Ŀ��
    public object  Payload;     // ��������
}

public static class EventBus
{
    private static readonly Dictionary<GameEvt, List<Action<GameEvent>>> _map = new();
    public static void Subscribe(GameEvt t, Action<GameEvent> cb) {
        if (!_map.TryGetValue(t, out var lst)) _map[t] = lst = new();
        lst.Add(cb);
    }
    public static void Unsubscribe(GameEvt t, Action<GameEvent> cb) {
        if (_map.TryGetValue(t, out var lst)) lst.Remove(cb);
    }
    public static void Publish(in GameEvent e) {
        if (_map.TryGetValue(e.Type, out var lst))
            // �򵥱�������Ƶ�ɸĳ� for
            foreach (var cb in lst) cb.Invoke(e);
    }
}
