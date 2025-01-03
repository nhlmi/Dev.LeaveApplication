﻿using Dev.LeaveApplication.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Dev.LeaveApplication.Data.Database;

public class LeaveApplicationDbContext : DbContext
{
	public LeaveApplicationDbContext(DbContextOptions<LeaveApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<FormModel> Applications { get; set; }
	public DbSet<EmployeeModel> Employees { get; set; }
	public DbSet<UserModel> Users { get; set; }
}
