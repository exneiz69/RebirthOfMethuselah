using System.Collections.Generic;
using UnityEngine.Events;

public class STObjectActionsParser
{
    public const string CloseActionKey = "Close";
    public const string PickUpActionKey = "Pick up";
    public const string IgniteActionKey = "Ignite";
    public const string WetActionKey = "Wet";
    public const string SmokeActionKey = "Smoke";
    public const string AccessActionKey = "Access";
    public const string DamageActionKey = "Damage";
    public const string RefuelActionKey = "Refuel";

    public IReadOnlyDictionary<string, UnityAction> Parse(STObject passive, UnityAction closeAction, UnityAction<IPickable> pickUpAction)
    {
        var parser = new Parser(closeAction, pickUpAction);
        Parse(passive, parser);
        return parser.Actions;
    }

    public IReadOnlyDictionary<string, UnityAction> Parse(STObject active, STObject passive, UnityAction closeAction, UnityAction<IPickable> pickUpAction)
    {
        var parser = new Parser(closeAction, pickUpAction);
        Parse(passive, parser);
        Parse(active, passive, parser);
        return parser.Actions;
    }

    private void Parse(STObject passive, Parser parser)
    {
        parser.Parse(passive);
        if (passive is IPickable pickable)
        {
            parser.Parse(pickable);
        }
    }

    private void Parse(STObject active, STObject passive, Parser parser)
    {
        if (active is IIgniter igniter && passive is IBurnable burnable)
        {
            parser.Parse(igniter, burnable);
        }
        if (active is IWetter wetter && passive is IWettable wettable)
        {
            parser.Parse(wetter, wettable);
        }
        if (active is ISmoker smoker && passive is ISmokable smokable)
        {
            parser.Parse(smoker, smokable);
        }
        if (active is ISignaler signaler && passive is ISignalable signalable)
        {
            parser.Parse(signaler, signalable);
        }
        if (active is IDamager damager && passive is IDamageable damageable)
        {
            parser.Parse(damager, damageable);
        }
        if (active is IRefueler refueler && passive is IRefuelable refuelable)
        {
            parser.Parse(refueler, refuelable);
        }
    }

    private class Parser
    {
        private readonly Dictionary<string, UnityAction> _actions = new Dictionary<string, UnityAction>();
        private readonly UnityAction _closeAction;
        private readonly UnityAction<IPickable> _pickUpAction;

        public Parser(UnityAction closeAction, UnityAction<IPickable> pickUpAction)
        {
            _closeAction = closeAction;
            _pickUpAction = pickUpAction;
        }

        public IReadOnlyDictionary<string, UnityAction> Actions => _actions;

        public void Parse(STObject obj)
        {
            if (_closeAction is null)
            {
                throw new System.InvalidOperationException();
            }
            else
            {
                var closeAction = _closeAction;
                _actions.Add(CloseActionKey, closeAction);
            }
        }

        public void Parse(IPickable obj)
        {
            if (_pickUpAction is null)
            {
                throw new System.InvalidOperationException();
            }
            else
            {
                var pickUpAction = _pickUpAction;
                _actions.Add(PickUpActionKey, () => pickUpAction(obj));
                var closeAction = _closeAction;
                _actions[PickUpActionKey] += closeAction;
            }
        }

        public void Parse(IIgniter active, IBurnable passive)
        {
            if (active.CanIgnite)
            {
                _actions.Add(IgniteActionKey, () => active.Ignite(passive));
                var closeAction = _closeAction;
                _actions[IgniteActionKey] += closeAction;
            }
        }

        public void Parse(IWetter active, IWettable passive)
        {
            if (active.CanWet)
            {
                _actions.Add(WetActionKey, () => active.Wet(passive));
                var closeAction = _closeAction;
                _actions[WetActionKey] += closeAction;
            }
        }

        public void Parse(ISmoker active, ISmokable passive)
        {
            if (active.CanSmoke)
            {
                _actions.Add(SmokeActionKey, () => active.Smoke(passive));
                var closeAction = _closeAction;
                _actions[SmokeActionKey] += closeAction;
            }
        }

        public void Parse(ISignaler active, ISignalable passive)
        {
            if (active.CanSignal)
            {
                _actions.Add(AccessActionKey, () => active.Signal(passive));
                var closeAction = _closeAction;
                _actions[AccessActionKey] += closeAction;
            }
        }

        public void Parse(IDamager active, IDamageable passive)
        {
            if (active.CanDamage)
            {
                _actions.Add(DamageActionKey, () => active.Damage(passive));
                var closeAction = _closeAction;
                _actions[DamageActionKey] += closeAction;
            }
        }

        public void Parse(IRefueler active, IRefuelable passive)
        {
            if (active.CanRefuel)
            {
                _actions.Add(RefuelActionKey, () => active.Refuel(passive));
                var closeAction = _closeAction;
                _actions[RefuelActionKey] += closeAction;
            }
        }
    }
}