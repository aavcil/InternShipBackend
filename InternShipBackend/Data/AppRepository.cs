using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Entities;
using InternShipBackend.Request;
using InternShipBackend.Response;
using Microsoft.EntityFrameworkCore;

namespace InternShipBackend.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;
        public AppRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {

            _context.Add(entity);

        }


        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public StudentResponse getStudent(int id)
        {
            try
            {
                var student = _context.Students.Include(x => x.Day).FirstOrDefault(x => x.id == id);
                var company = _context.Companies.FirstOrDefault(x => x.id == student.companyId);
                if (student != null)
                {
                    var studentResponse = new StudentResponse()
                    {
                        tcNo = student.tcNo,
                        Day = student.Day,
                        id = student.id,
                        surname = student.surname,
                        name = student.name,
                        Companies = company,
                        profilePicture = student.profilePicture,
                    };
                    return studentResponse;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public List<StudentResponse> GetStudents()
        {
            var students = _context.Students.Include(x => x.Company).Include(x => x.Day).Select(x => new StudentResponse()
            {
                name = x.name,
                id = x.id,
                Day = x.Day,
                profilePicture = x.profilePicture,
                surname = x.surname,
                tcNo = x.tcNo,
                Companies = x.Company
            }).ToList();

            return students;
        }

        public List<RecourseResponse> GetRecourses()
        {
            try
            {
                return _context.Recourses.Include(x => x.Student).Select(x => new RecourseResponse()
                {
                    name = x.Student.name,
                    id = x.id,
                    url = x.url,
                    surname = x.Student.surname,
                    studentId = x.studentId,
                    date = x.date,
                    isApproved = x.isApproved
                }).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<RecourseResponse> GetRecoursesFalse()
        {
            try
            {
                return _context.Recourses.Where(x => x.isApproved == 0).Include(x => x.Student).Select(x =>
                    new RecourseResponse()
                    {
                        name = x.Student.name,
                        id = x.id,
                        url = x.url,
                        surname = x.Student.surname,
                        studentId = x.studentId,
                        date = x.date,
                        isApproved = x.isApproved
                    }).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<StudentResponseForAcademician> GetStudentForHome()
        {
            try
            {
                return _context.Students.Select(x => new StudentResponseForAcademician()
                {
                    id = x.id,
                    name = x.name,
                    profilePicture = x.profilePicture,
                    surname = x.surname,
                    tcNo = x.tcNo
                }).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public List<Teacher> getTeachers()
        {
            return _context.Teachers.ToList();

        }

        public List<AssignmentResponse> getAssignment(int teacherId)
        {
            var assignment = _context.Assignments.Where(x => x.teacherId == teacherId).Include(x => x.Student)
                .Include(x => x.Teachers).Select(x => new AssignmentResponse
                {
                    teacherId = x.teacherId,
                    studentId = x.studentId,
                    Teachers = x.Teachers,
                    Student = x.Student,
                    id = x.id
                }).ToList();


            return assignment;
        }

        public CompanyResponse getCompany(int id)
        {
            var company = _context.Companies.FirstOrDefault(x => x.id == id);
            if (company != null)
            {
                var companyResponse = new CompanyResponse()
                {
                    id = company.id,
                    name = company.name,
                    address = company.address,
                    logoUrl = company.logoUrl,
                    mail = company.mail,
                    personelCount = company.personelCount,
                    telephone = company.telephone
                };
                return companyResponse;
            }

            return null;
        }

        public RecourseResponse getRecourse(int id)
        {
            try
            {
                var rec = _context.Recourses.FirstOrDefault(x => x.studentId == id);
                var stu = getStudent(id);
                if (rec != null)
                {
                    var resp = new RecourseResponse()
                    {
                        name = stu.name,
                        id = rec.id,
                        url = rec.url,
                        surname = stu.surname,
                        date = rec.date,
                        isApproved = rec.isApproved
                    };
                    return resp;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<CompanyResponse> getAllCompanies()
        {
            var comp = _context.Companies.Select(x => new CompanyResponse()
            {
                name = x.name,
                id = x.id,
                address = x.address,
                logoUrl = x.logoUrl,
                mail = x.mail,
                personelCount = x.personelCount,
                telephone = x.telephone
            }).ToList();
            return comp;
        }

        public bool setRecourse(int id)
        {
            try
            {
                var rec = _context.Recourses.FirstOrDefault(x => x.studentId == id);
                if (rec != null)
                {
                    rec.isApproved = 1;

                    return SaveAll();
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool setRecourseReject(int id)
        {
            try
            {
                var rec = _context.Recourses.FirstOrDefault(x => x.studentId == id);
                if (rec != null)
                {
                    rec.isApproved = 2;

                    return SaveAll();
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public DateTime[] createDays(InternShipRequest req)
        {
            DateTime firstDay = req.startDate.Date;
            DateTime lastDay = req.finishDate.Date;
            //DateTime[] bankHolidays;
            if (firstDay > lastDay)
                throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = (int)firstDay.DayOfWeek;
                int lastDayOfWeek = (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            //foreach (DateTime bankHoliday in bankHolidays)
            //{
            //    DateTime bh = bankHoliday.Date;
            //    if (firstDay <= bh && bh <= lastDay)
            //        --businessDays;
            //}

            DateTime[] dateTimes = new DateTime[businessDays];
            DateTime now = firstDay;
            for (int i = 0; i <= (lastDay - firstDay).TotalDays; i++)
            {
                if ((now.DayOfWeek != DayOfWeek.Saturday) && (now.DayOfWeek != DayOfWeek.Sunday))
                {
                    //dateTimes[i] = now;

                    var day = new Day()
                    {
                        studentId = req.studentId,
                        date = now
                    };
                    _context.Add(day);
                    _context.SaveChanges();
                }

                now = now.AddDays(1);
            }
            return dateTimes;
        }

        public TeacherResponse getTeacher(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(x => x.id == id);
            if (teacher != null)
            {
                var teacherResponse = new TeacherResponse()
                {
                    id = teacher.id,
                    surname = teacher.surname,
                    name = teacher.name,
                    title = teacher.title,
                    userGroup = teacher.userGroup
                };

                return teacherResponse;
            }

            return null;
        }

        public bool setCompanyId(int id, int companyId)
        {
            var student = _context.Students.FirstOrDefault(x => x.id == id);
            if (student != null) student.companyId = companyId;
            return SaveAll();
        }

        public bool assignment()
        {
            var teachers = getTeachers();
            var students = GetStudents();
            int teacherCount = teachers.Count;
            int studentCount = students.Count;
            int x = -1;

            int remaining = studentCount % teacherCount;
            for (int i = 0; i < teacherCount; i++)
            {
                for (int j = 0; j < studentCount / teacherCount; j++)
                {
                    var assignment = new Assignment()
                    {
                        teacherId = teachers[i].id,
                        studentId = students[++x].id
                    };

                    _context.Add(assignment);
                    _context.SaveChanges();
                }

            }

            for (int i = 0; i < remaining; i++)
            {
                var assignment = new Assignment()
                {
                    teacherId = teachers[i].id,
                    studentId = students[++x].id
                };
                _context.Add(assignment);
                _context.SaveChanges();
            }

            return true;

        }

        public bool editStudent(string image, int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.id == id);
            if (student != null) student.profilePicture = image;
            return SaveAll();
        }

        public bool editDay(DayRequest day)
        {
            var getDay = _context.Days.FirstOrDefault(x => x.id == day.id);
            if (getDay != null)
            {
                getDay.title = day.title;
                getDay.description = day.description;
            }

            return SaveAll();
        }

        public bool isInternShipOK(int id)
        {
            var internShip = _context.InternShips.FirstOrDefault(x => x.studentId == id);

            if (internShip != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GradingSettings(int num)
        {
            var grad = _context.GradingSettings.First();

            if (num == 0)
            {
                grad.vize = true;
                grad.final = false;
            }
            else if (num == 1)
            {
                grad.vize = false;
                grad.final = true;
            }
            else
            {
                grad.vize = false;
                grad.final = false;
            }

            return SaveAll();
        }

        public bool IsMyStudent(int studentId, int academicianId)
        {
            var students = _context.Assignments.Where(x => x.teacherId == academicianId).ToList();

            var isExist = students.Find(r => r.studentId == studentId);
            if (isExist != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<SpecificDays> GetDates()
        {
            var date = _context.SpecificDays.ToList();
            return date;
        }

        public DayResponseForModal GetDay(int id)
        {
            var day = _context.Days.Include(x => x.Photos).FirstOrDefault(x => x.id == id);
            if (day != null)
            {
                var res = new DayResponseForModal()
                {
                    Photo = day.Photos,
                    title = day.title,
                    description = day.description

                };
                return res;
            }

            return null;
        }

        public GradingSetting GetSetting()
        {
            var set = _context.GradingSettings.First();
            return set;
        }

        public bool SetGrade(GradeRequest req)
        {
            var grade = _context.Grades.FirstOrDefault(x => x.studentId == req.studentId);
            if (grade != null)
            {
                if (req.vize != 0)
                    grade.vize = req.vize;
                if (req.final != 0)
                    grade.final = req.final;
            }
            else
            {
                var rec = new Grade()
                {
                    studentId = req.studentId,
                    final = req.final,
                    vize = req.vize
                };
                Add(rec);
            }
            return SaveAll();
        }

        public bool isGradeExist(int id)
        {
            var grade = _context.Grades.FirstOrDefault(x => x.studentId == id);
            if (grade != null)
            {
                return true;
            }

            return false;
        }

        public GradeResponse GetGradeResponse(int id)
        {
            var grade = _context.Grades.FirstOrDefault(x => x.studentId == id);
            if (grade != null)
            {
                var g = new GradeResponse()
                {
                    final = grade.final,
                    vize = grade.vize

                };
                return g;
            }
            var w = new GradeResponse()
            {
                final = 0,
                vize = 0

            };
            return w;

        }


        public List<DayResponse> getDaysByStudent(int id)
        {
            var days = _context.Days.Include(x => x.Photos).Where(x => x.studentId == id).Select(x => new DayResponse()
            {
                id = x.id,
                studentId = x.studentId,
                description = x.description,
                url = x.url,
                date = x.date,
                title = x.title,
                Photos = x.Photos
            }).ToList();
            return days;
        }

        public FileResponse GetFileResponse(int id)
        {
            var file = _context.Files.FirstOrDefault(x => x.studentId == id);
            if (file != null)
            {
                var res = new FileResponse()
                {
                    id = file.id,
                    studentId = file.studentId,
                    path = file.path
                };
                return res;
            }

            return null;
        }

        public AssignmentResponseForStudent GetAssignmentForStudent(int id)
        {
            var assignment = _context.Assignments.Include(x => x.Teachers).FirstOrDefault(x => x.studentId == id);
            if (assignment != null)
            {
                var forStudent = new AssignmentResponseForStudent()
                {
                    name = assignment.Teachers.name,
                    surname = assignment.Teachers.surname,
                    title = assignment.Teachers.title
                };
                return forStudent;
            }

            return null;
        }

        public List<AssignmentResponse> GetAllAssignment()
        {
            return _context.Assignments.Include(x => x.Student).Include(x => x.Teachers).Select(x =>
                new AssignmentResponse()
                {
                    studentId = x.studentId,
                    Teachers = x.Teachers,
                    Student = x.Student,
                    teacherId = x.teacherId,
                    id = x.id

                }).ToList();
        }

        public List<StudentResponseForAcademician> GetStudentsForAcademicians(int id)
        {
            return _context.Assignments.Include(x => x.Student).Where(x => x.teacherId == id).Select(x =>
                new StudentResponseForAcademician()
                {
                    name = x.Student.name,
                    id = x.studentId,
                    profilePicture = x.Student.profilePicture,
                    surname = x.Student.surname,
                    tcNo = x.Student.tcNo
                }).ToList();
        }
    }
}
