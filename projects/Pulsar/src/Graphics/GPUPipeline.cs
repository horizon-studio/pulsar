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
        private GPUResource _vao;
        private GPUBuffer[] _buffers;
        private IntPtr[] _bufferPtrs;
        private GPUBuffer _indices;
        private IntPtr _indicePtr;
        private GPUFramebuffer _framebuffer;
        public GPUPipeline(GPUContext context, GPUShaderProgram shaderProgram, GPUResource vao, GPUBuffer[] buffers, GPUBuffer indices)
        {
            _context = context;
            _program = shaderProgram;
            _vao = vao;
            _buffers = buffers;
            _indices = indices;
            _bufferPtrs = new IntPtr[buffers.Length];
            for (var i = 0; i < _bufferPtrs.Length; i++)
            {
                //_bufferPtrs[i] = Gl.glMapNamedBuffer(_buffers[i].GetHandle(), Gl.GL_WRITE_ONLY);
                //Console.WriteLine("adresse :" + _bufferPtrs[i] + "  error : " + Gl.glGetError());
            }
            //if (indices != null)
                //_indicePtr = Gl.glMapNamedBuffer(_indices.GetHandle(), Gl.GL_WRITE_ONLY);
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

        public void UpdateIndices(uint destOffset, byte[] data, uint length)
        {
            Marshal.Copy(data, 0, _indicePtr + (int)destOffset, (int)length);
        }

        public void UpdateData(uint indexBuffer, uint destOffset, byte[] data, uint length)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            Gl.glNamedBufferSubData(_buffers[0].GetHandle(), 0, (uint)data.Length, handle.AddrOfPinnedObject());
            handle.Free();
        }

        public void Draw(GPUDrawCommand command)
        {
            Matrix m = Matrix.PerspectiveFovLH(70, 2.6f, 0.1f, 10);
            //TODO: this is debug impl , check if already binded
            //Gl.glBindFramebuffer(Gl.GL_FRAMEBUFFER, _framebuffer.GetHandle());
            Gl.glUseProgram(_program.GetHandle());
            Gl.glBindVertexArray(_vao.GetHandle());
            Gl.glDrawArrays((int)command.Mode, (int)command.FirstVertex, (int)command.VertexCount);
        }

        public void SetFramebuffer(GPUFramebuffer framebuffer)
        {
            _framebuffer = framebuffer;
        }
    }
}