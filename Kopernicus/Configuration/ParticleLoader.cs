﻿/**
 * Kopernicus Planetary System Modifier
 * ====================================
 * Created by: BryceSchroeder and Teknoman117 (aka. Nathaniel R. Lewis)
 * Maintained by: Thomas P., NathanKell and KillAshley
 * Additional Content by: Gravitasi, aftokino, KCreator, Padishar, Kragrathea, OvenProofMars, zengei, MrHappyFace
 * ------------------------------------------------------------- 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301  USA
 * 
 * This library is intended to be used as a plugin for Kerbal Space Program
 * which is copyright 2011-2015 Squad. Your usage of Kerbal Space Program
 * itself is governed by the terms of its EULA, not the license above.
 * 
 * https://kerbalspaceprogram.com
 */
 
using System.Collections.Generic;
using Kopernicus.Components;
using UnityEngine;

namespace Kopernicus
{
    namespace Configuration
    {
        [RequireConfigType(ConfigType.Node)]
        public class ParticleLoader : BaseLoader, IParserEventSubscriber
        {
            // Set-up our parental objects
            PlanetParticleEmitter particle;
            GameObject scaledVersion;

            // Target of our particles
            [ParserTarget("target", optional = true, allowMerge = false)]
            public string target
            {
                get { return particle.target; }
                set { particle.target = value; }
            }

            // minEmission of particles
            [ParserTarget("minEmission", optional = true, allowMerge = false)]
            public NumericParser<float> minEmission
            {
                get { return particle.minEmission; }
                set { particle.minEmission = value; }
            }

            // maxEmission of particles
            [ParserTarget("maxEmission", optional = true, allowMerge = false)]
            public NumericParser<float> maxEmission
            {
                get { return particle.maxEmission; }
                set { particle.maxEmission = value; }
            }

            // minimum lifespan of particles
            [ParserTarget("lifespanMin", optional = true, allowMerge = false)]
            public NumericParser<float> lifespanMin
            {
                get { return particle.minEnergy; }
                set { particle.minEnergy = value; }
            }

            // maximum lifespan of particles
            [ParserTarget("lifespanMax", optional = true, allowMerge = false)]
            public NumericParser<float> lifespanMax
            {
                get { return particle.maxEnergy; }
                set { particle.maxEnergy = value; }
            }

            // minimum size of particles
            [ParserTarget("sizeMin", optional = true, allowMerge = false)]
            public NumericParser<float> sizeMin
            {
                get { return particle.minSize; }
                set { particle.minSize = value; }
            }

            // maximum size of particles
            [ParserTarget("sizeMax", optional = true, allowMerge = false)]
            public NumericParser<float> sizeMax
            {
                get { return particle.maxSize; }
                set { particle.maxSize = value; }
            }

            // speedScale of particles
            [ParserTarget("speedScale", optional = true, allowMerge = false)]
            public NumericParser<float> speedScaleLoader
            {
                get { return particle.speedScale; }
                set { particle.speedScale = value; }
            }

            // grow rate of particles
            [ParserTarget("rate", optional = true, allowMerge = false)]
            public NumericParser<float> rate
            {
                get { return particle.sizeGrow; }
                set { particle.sizeGrow = value; }
            }

            // rand Velocity of particles
            [ParserTarget("randVelocity", optional = true, allowMerge = false)]
            public Vector3Parser randVelocity
            {
                get { return particle.randomVelocity; }
                set { particle.randomVelocity = value; }
            }

            // Texture of particles
            [ParserTarget("texture", optional = true, allowMerge = false)]
            public Texture2DParser texture
            {
                get { return particle.mainTexture as Texture2D; }
                set { particle.mainTexture = value; }
            }

            // Default Constructor
            public ParticleLoader()
            {
                scaledVersion = generatedBody.scaledVersion;
            }

            // Runtime constructor
            public ParticleLoader(CelestialBody body)
            {
                scaledVersion = body.scaledBody;
            }

            // Apply event
            void IParserEventSubscriber.Apply(ConfigNode node)
            {
                if (particle == null)
                    particle = scaledVersion.AddComponent<PlanetParticleEmitter>();
            }

            // Post-Apply event
            void IParserEventSubscriber.PostApply(ConfigNode node)
            {
                List<Color> colors = new List<Color>();
                foreach (string color in node.GetNode("Colors").GetValuesStartsWith("color"))
                {
                    Vector4 c = ConfigNode.ParseVector4(color);
                    colors.Add(new Color(c.x, c.y, c.z, c.w));
                }
                particle.colorAnimation = colors.ToArray();
            }

        }
    }
}
