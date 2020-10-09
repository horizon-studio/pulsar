using System;
using System.Runtime.InteropServices;
using System.Threading;
using Pulsar.Contexts.OpenGL;
using Pulsar.Maths;

namespace Pulsar.Graphics
{
    public class GPUPipeline
    {
        private GPUEngine _engine;
        private GPUShaderProgram _program;
        private GPUPipelineFormat _format;
        private GPUBuffer[] _buffers;

        public GPUPipeline(GPUEngine engine, GPUShaderProgram shaderProgram, GPUPipelineFormat format, GPUBuffer[] buffers)
        {
            _engine = engine;
            _program = shaderProgram;
            _format = format;
            _buffers = buffers;
        }
        
        public void SetUniform(string name, int value)
        {
            throw new System.NotImplementedException();
        }

        public void SetUniform(string name, float value)
        {
            throw new System.NotImplementedException();
        }

        public void SetUniform(string name, Vector2 value)
        {
            throw new System.NotImplementedException();
        }

        public void SetUniform(string name, Matrix mat)
        {
            _engine.BindShaderProgram(_program);
            Gl.glUniformMatrix4fv(Gl.glGetUniformLocation(_program, name), 1, false, mat.ToArray());    
        }
        
        public void UpdateData(uint indexBuffer, uint destOffset, byte[] data, uint length)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            Gl.glNamedBufferSubData(_buffers[indexBuffer], 0, (uint)data.Length, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void Draw(GPUDrawCommand command)
        {/*
            for (int i = 0; i < _format.GetBufferFormats().Length; i++)
            {
                GPUBufferFormat bf = _format.GetBufferFormats()[i];
                Gl.glVertexArrayVertexBuffer(_format, bf.BufferIndex, _buffers[i], bf.Offset, (int)bf.Stride);
            }*/
            _engine.BindShaderProgram(_program);
            _engine.BindFormat(_format);
            _engine.ReadErrors();
            command.Draw();
        }
    }
}