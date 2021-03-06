using RLBotModels.Command;
using RLBotModels.Control;
using RLBotModels.Phys;

namespace RLBotSecret.Conversion
{
    internal class FlatToModel
    {
        static internal CarInput ToCarInput(rlbot.flat.ControllerState state)
        {
            float dodgeForward = -state.Pitch;

            // Setting strafe = yaw allows us to use the "stall" mechanic as expected.
            float dodgeStrafe = state.Yaw;

            // TODO: consider clamping all the values between -1 and 1. Old RLBot did that.

            return new CarInput()
            {
                throttle = state.Throttle,
                steer = state.Steer,
                pitch = state.Pitch,
                yaw = state.Yaw,
                roll = state.Roll,
                jump = state.Jump,
                boost = state.Boost,
                handbrake = state.Handbrake,
                useItem = state.UseItem,
                dodgeForward = dodgeForward,
                dodgeStrafe = dodgeStrafe,
            };
        }

        static internal Vector3 ToVector(rlbot.flat.Vector3 vec)
        {
            return new Vector3() { x = vec.X, y = vec.Y, z = vec.Z };
        }

        static internal Rotator ToRotator(rlbot.flat.Rotator r)
        {
            return new Rotator() { pitch = r.Pitch, yaw = r.Yaw, roll = r.Roll };
        }

        static internal Loadout ToLoadout(rlbot.flat.PlayerLoadout l, int team)
        {
            System.Drawing.Color primaryColor;
            if (l.PrimaryColorLookup.HasValue)
            {
                var c = l.PrimaryColorLookup.Value;
                primaryColor = System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
            } else
            {
                primaryColor = ColorSwatches.GetPrimary(l.TeamColorId, team);
            }

            System.Drawing.Color secondaryColor;
            if (l.SecondaryColorLookup.HasValue)
            {
                var c = l.SecondaryColorLookup.Value;
                secondaryColor = System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
            }
            else
            {
                secondaryColor = ColorSwatches.GetSecondary(l.CustomColorId);
            }


            return new Loadout()
            {
                carId = (ushort)l.CarId,
                antennaId = (ushort)l.AntennaId,
                boostId = (ushort)l.BoostId,
                engineAudioId = (ushort)l.EngineAudioId,
                customFinishId = (ushort)l.CustomFinishId,
                decalId = (ushort)l.DecalId,
                goalExplosionId = (ushort)l.GoalExplosionId,
                hatId = (ushort)l.HatId,
                paintFinishId = (ushort)l.PaintFinishId,
                trailsId = (ushort)l.TrailsId,
                wheelsId = (ushort)l.WheelsId,
                primaryColorLookup = primaryColor,
                secondaryColorLookup = secondaryColor
            };
        }
    }
}
