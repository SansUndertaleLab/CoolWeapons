using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace CoolWeapons.Content.Projectiles
{
    public class Spark : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.friendly = false;
            Projectile.hostile = false;

            Projectile.DamageType = DamageClass.Magic;

            Projectile.aiStyle = ProjAIStyleID.Arrow;
        }

        public static NPC GetNearestNPC(float range)
        {
            NPC nearestNPC = null;
            float nearest_distance = range * range;

            for(int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if(npc.CanBeChasedBy())
                {
                    float distance = Vector2.DistanceSquared(npc.Center, npc.Center);
                    if(distance < nearest_distance)
                    {
                        nearestNPC = npc;
                        nearest_distance = distance;
                    }
                }
            }

            return nearestNPC;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            NPC nearest_npc = GetNearestNPC(10f);
            Projectile spike = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, new Vector2(0f, -10f), ModContent.ProjectileType<Spike>(), 20, 1f);

            ((Spike)spike.ModProjectile).targeted_npc = nearest_npc;

            return true;
        }
    }
}
