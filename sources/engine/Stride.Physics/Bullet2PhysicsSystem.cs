// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org)
// Copyright (c) 2018-2021 Stride and its contributors (https://stride3d.net)
// Copyright (c) 2011-2018 Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// See the LICENSE.md file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;

using Stride.Core;
using Stride.Engine;
using Stride.Engine.Design;
using Stride.Games;

namespace Stride.Physics
{
    public class Bullet2PhysicsSystem : GameSystem, IPhysicsSystem
    {
        private class PhysicsScene
        {
            public PhysicsProcessor Processor;
            public Simulation Simulation;
        }

        private readonly List<PhysicsScene> scenes = new List<PhysicsScene>();

        static Bullet2PhysicsSystem()
        {
            // Preload proper libbulletc native library
            NativeLibraryHelper.Load("libbulletc", typeof(Bullet2PhysicsSystem));
        }

        public Bullet2PhysicsSystem(IServiceRegistry registry)
            : base(registry)
        {
            // Make sure physics runs before everything
            UpdateOrder = -1000;

            // Enabled by default
            Enabled = true;
        }

        private PhysicsSettings physicsConfiguration;

        public override void Initialize()
        {
            var gameSettings = Services.GetService<IGameSettingsService>()?.Settings;
            physicsConfiguration = gameSettings?.Configurations?.Get<PhysicsSettings>() ?? new PhysicsSettings();
        }

        protected override void Destroy()
        {
            base.Destroy();

            lock (this)
            {
                foreach (var scene in scenes)
                {
                    scene.Simulation.Dispose();
                }
            }
        }

        public Simulation Create(PhysicsProcessor sceneProcessor, PhysicsEngineFlags flags = PhysicsEngineFlags.None)
        {
            var scene = new PhysicsScene
            {
                Processor = sceneProcessor,
                Simulation = new Simulation(sceneProcessor, physicsConfiguration)
            };

            lock (this)
            {
                scenes.Add(scene);
            }
            return scene.Simulation;
        }

        public void Release(PhysicsProcessor processor)
        {
            lock (this)
            {
                var scene = scenes.SingleOrDefault(x => x.Processor == processor);
                if (scene is null)
                    return;

                scenes.Remove(scene);
                scene.Simulation.Dispose();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Simulation.DisableSimulation)
                return;

            lock (this)
            {
                foreach (var physicsScene in scenes)
                {
                    // First process any needed cleanup
                    physicsScene.Processor.UpdateRemovals();

                    // Read skinned meshes bone positions and write them to the physics engine
                    physicsScene.Processor.UpdateBones();

                    // Simulate physics
                    physicsScene.Simulation.Simulate((float)gameTime.WarpElapsed.TotalSeconds);

                    // Update character bound Entity's Transforms from physics engine simulation
                    physicsScene.Processor.UpdateCharacters();

                    // Perform cleanup before test contacts in this frame
                    physicsScene.Simulation.BeginContactTesting();

                    // Handle frame contacts
                    physicsScene.Processor.UpdateContacts();

                    // This is the heavy contact logic
                    physicsScene.Simulation.EndContactTesting();

                    // Send contact events
                    physicsScene.Simulation.SendEvents();
                }
            }
        }
    }
}
