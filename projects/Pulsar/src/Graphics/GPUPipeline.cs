using System;
using System.Runtime.InteropServices;
using Pulsar.Contexts.OpenGL;
using Pulsar.Maths;

namespace Pulsar.Graphics
{
    public class GPUPipeline
    {
        private GPUContext _context;
        private GPUShaderProgram _program;
        private GPUPipelineFormat _format;
        private GPUBuffer[] _buffers;

        public GPUPipeline(GPUContext context, GPUShaderProgram shaderProgram, GPUPipelineFormat format, GPUBuffer[] buffers)
        {
            _context = context;
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

        public void UpdateData(uint indexBuffer, uint destOffset, byte[] data, uint length)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            Gl.glNamedBufferSubData(_buffers[0].GetHandle(), 0, (uint)data.Length, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void Draw(GPUDrawCommand command)
        {
            for (int i = 0; i < _format.GetBufferFormats().Length; i++)
            {
                Gl.glVertexArrayVertexBuffer(_format, _format.GetBufferFormats()[i].BufferIndex, _buffers[i], _format.GetBufferFormats()[i].Offset, (int)_format.GetBufferFormats()[i].Stride);
                i++;
            }
            _context.BindShaderProgram(_program);
            _context.BindFormat(_format);
            Gl.glDrawArrays((int)command.Mode, (int)command.FirstVertex, (int)command.VertexCount);
        }
    }
}