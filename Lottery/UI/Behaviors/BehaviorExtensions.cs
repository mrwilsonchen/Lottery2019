﻿using FlysEngine.Sprites;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery2019.UI.Behaviors
{
    public abstract class BehaviorExtensions
    {
        private static Dictionary<string, Func<Behavior>> BehaviorMap
            = new Dictionary<string, Func<Behavior>>
            {
                [nameof(RotationBehavior)] = () => new RotationBehavior(),
                [nameof(AutoFrameBehavior)] = () => new AutoFrameBehavior(),
                [nameof(CircleCreatorBehavior)] = () => new CircleCreatorBehavior(),
                [nameof(ButtonBehavior)] = () => new ButtonBehavior(),
                [nameof(AutoBorderBehavior)] = () => new AutoBorderBehavior(),
                [nameof(BoxOpenBehavior)] = () => new BoxOpenBehavior(),
                [nameof(AccelerateBehavior)] = () => new AccelerateBehavior(),
                [nameof(MarqueeBehavior)] = () => new MarqueeBehavior(),
                [nameof(MarqueeGroupBehavior)] = () => new MarqueeGroupBehavior(),
                [nameof(GroupItemBehavior)] = () => new GroupItemBehavior(),
                [nameof(AccelerateGroupBehavior)] = () => new AccelerateGroupBehavior(),
                [nameof(ThunderBehavior)] = () => new ThunderBehavior(),
                [nameof(SensorBehavior)] = () => new SensorBehavior(),
                [nameof(RestitutionBehavior)] = () => new RestitutionBehavior(), 
            };

        public static Behavior Create(string behaviorName, Dictionary<string, object> options)
        {
            if (BehaviorMap.ContainsKey(behaviorName))
            {
                var behavior = BehaviorMap[behaviorName]();
                var type = behavior.GetType();
                foreach (var option in options)
                {
                    var prop = type.GetProperty(option.Key);
                    prop.SetValue(behavior, SwitchType(option.Value, prop.PropertyType));
                }
                return behavior;
            }
            throw new KeyNotFoundException(behaviorName);
        }

        private static object SwitchType(object v, Type targetType)
        {
            if (v is double d)
            {
                return (float)d;
            }
            else if (v is long di)
            {
                if (targetType == typeof(int))
                {
                    return (int)di;
                }
                else if (targetType == typeof(float))
                {
                    return (float)di;
                }
            }
            else if (v is JArray ds)
            {
                return ds.Select(x => (float)x).ToArray();
            }

            throw new NotImplementedException();
        }
    }
}
