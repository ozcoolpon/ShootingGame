using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShootingGame.Particle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


/*
 **
 Reference: From http://xbox.create.msdn.com/en-US/education/catalog/sample/particle_3d
 * // Copyright (c) 2007-2011 dhpoware. All Rights Reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR
 */
namespace ShootingGame.GameComponent
{
    public class ExplosionHandler : Microsoft.Xna.Framework.GameComponent
    {

        ParticleSystem explosionParticles;
        ParticleSystem projectTrialParticles;
        List<Explosion> explosions = new List<Explosion>();

        /// <summary>
        /// Constructs a new projectile.
        /// </summary>
        public ExplosionHandler(Game game)
            : base(game)
        {
            explosionParticles = new ExplosionParticleSystem(game, game.Content);
            projectTrialParticles = new ProjectileTrailParticleSystem(game, game.Content);
            explosionParticles.DrawOrder = 2;
            game.Components.Add(explosionParticles);
            game.Components.Add(projectTrialParticles);
        }

        public void CreateExplosion(Vector3 position)
        {
            explosions.Add(new Explosion(explosionParticles,
                                               projectTrialParticles, position));
        }


        /// <summary>
        /// Updates the explosion.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            int i = 0;

            while (i < explosions.Count)
            {
                if (!explosions[i].Update(gameTime))
                {
                    // Remove projectiles at the end of their life.
                    explosions.RemoveAt(i);
                }
                else
                {
                    // Advance to the next projectile.
                    i++;
                }
            }
        }

        public void Draw(GameTime gameTime, FirstPersonCamera camera)
        {
            explosionParticles.SetCamera(camera.ViewMatrix, camera.ProjectionMatrix);
            projectTrialParticles.SetCamera(camera.ViewMatrix, camera.ProjectionMatrix);
        }
    }

}
