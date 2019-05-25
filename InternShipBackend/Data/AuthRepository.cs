using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternShipBackend.Data
{
    public class AuthRepository : IAuthRepository
    {
        private DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password,int userId)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.userId = userId;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Student> StudentRegister(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            
            return await _context.Students.FirstOrDefaultAsync(x => x.tcNo == student.tcNo);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == userName);
            if (user == null)
            {

                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _context.Users.AnyAsync(x => x.Username == userName))
            {
                return true;
            }
            return false;
        }
        public async Task<bool> TeacherExists(string userName)
        {
            if (await _context.TeachersLogins.AnyAsync(x => x.Username == userName))
            {
                return true;
            }
            return false;
        }

        public long studentId(int id)
        {
            var student = _context.Users.FirstOrDefault(x => x.id == id);
            if (student != null) return student.userId;
            return 0;
        }

        public async Task<TeachersLogin> TeacherRegister(TeachersLogin teacher, string password, int teacherId)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            teacher.PasswordHash = passwordHash;
            teacher.PasswordSalt = passwordSalt;
            teacher.teacherId = teacherId;

            await _context.TeachersLogins.AddAsync(teacher);
            await _context.SaveChangesAsync();

            return teacher;
        }

        public async Task<Teacher> TeacherCreate(Teacher teachers)
        {
            await _context.Teachers.AddAsync(teachers);
            await _context.SaveChangesAsync();

            return await _context.Teachers.FirstOrDefaultAsync(x => x.id ==teachers.id );
        }

        public async Task<TeachersLogin> TeacherLogin(string userName, string password)
        {
            var user = await _context.TeachersLogins.FirstOrDefaultAsync(x => x.Username == userName);
            if (user == null)
            {

                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }
    }
}
