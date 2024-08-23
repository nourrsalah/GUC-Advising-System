A comprehensive database system was developed to manage student advising, course registration, payments, and administrative functions. The system's functionality is summarized as follows:

Table Management:

CreateAllTables: Creates all necessary tables for the advising system.
DropAllTables: Drops all tables from the database, allowing for a reset if needed.
ClearAllTables: Clears all records from existing tables without affecting the schema.
Data Retrieval via Views:

view_Students: Fetches details of all active students for an overview of the student population.
view_Course_prerequisites: Displays courses along with their prerequisites.
Instructors_AssignedCourses: Shows instructors with their assigned courses.
Student_Payment: Provides details of payments and corresponding students.
Courses_Slots_Instructor: Combines course, slot, and instructor details into one table.
Courses_MakeupExams: Lists courses with their makeup exam details.
Students_Courses_transcript: Compiles student course history, including grades and instructors.
Semster_offered_Courses: Lists semesters along with their offered courses.
Advisors_Graduation_Plan: Details graduation plans with their initiated advisors.
Administrative Functions via Stored Procedures:

Handles student and advisor registration, listing students and advisors, adding semesters and courses, linking instructors to courses, linking students to courses and advisors, adding exams, issuing installments, deleting courses and slots, and updating student statuses based on financial information.
Enhanced Functionality via Functions:

Login functions: Manages login processes for students and advisors.
Data retrieval functions: Fetches pending requests, available courses, graduation plans, upcoming installments, and course slots taught by specific instructors.
This system efficiently manages the various operations involved in student advising, course registration, payments, and administration, demonstrating proficiency in SQL, including complex queries, stored procedures, and views.
