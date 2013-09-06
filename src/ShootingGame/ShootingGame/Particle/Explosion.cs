using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

/*
 Reference: From http://xbox.create.msdn.com/en-US/education/catalog/sample/particle_3d
 * */

// Copyright (c). All Rights Reserved.
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
namespace ShootingGame.Particle
{
    class Explosion
    {
      
            #region Constants

            const float trailParticlesPerSecond = 200;
            const int numExplosionParticles = 30;
            const int numExplosionSmokeParticles = 50;
            const float projectileLifespan = 0f;
            const float sidewaysVelocityRange = 60;
            const float verticalVelocityRange = 40;
            const float gravity = 15;

            #endregion

            #region Fields

            ParticleSystem explosionParticles;
            ParticleEmitter trailEmitter;

            Vector3 position;
            float age;
        
            #endregion


            /// <summary>
            /// Constructs a new projectile.
            /// </summary>
            public Explosion(ParticleSystem explosionParticles,
                              ParticleSystem projectileTrailParticles, Vector3 position)
            {
                this.explosionParticles = explosionParticles;

                // Start at the origin, firing in a random (but roughly upward) direction.
                this.position = position;
                // Use the particle emitter helper to output our trail particles.
                trailEmitter = new ParticleEmitter(projectileTrailParticles,
                                                  trailParticlesPerSecond, position);
            }


            /// <summary>
            /// Updates the projectile.
            /// </summary>
            public bool Update(GameTime gameTime)
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Simple projectile physics.
                age += elapsedTime;

                // Update the particle emitter, which will create our particle trail.
                trailEmitter.Update(gameTime, position);

                // If enough time has passed, explode! Note how we pass our velocity
                // in to the AddParticle method: this lets the explosion be influenced
                // by the speed and direction of the projectile which created it.
                if (age > projectileLifespan)
                {
                    for (int i = 0; i < numExplosionParticles; i++)
                    {
                        explosionParticles.AddParticle(position, Vector3.Zero);
                    }
                    return false;
               }

                return true;
            }
        }
    
}
