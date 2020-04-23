# MySQL Recipe for Entity Framework

- ```dotnet add package MySql.Data.EntityFrameworkCore --version 8.0.19```
- In the class that derives from ```DbContext``` override the ```OnConfiguring``` method to set the MySQL data provider with ```UseMySQL```.

```C#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
     // Warning To protect potentially sensitive information in your connection string,
     // you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263
     // for guidance on storing connection strings.
     optionsBuilder.UseMySQL("server=142.93.114;database=donbstringham;user=donbstringham;password=letmein");
}
```

- Create a new file named ```MySqlContext.cs``` and add the following code to it:

```C#
using Microsoft.EntityFrameworkCore;
using BlabberApp.Domain.Entities;

namespace BlabberApp.DataStore.Plugins
{
    public class MySqlContext : DbContext
    {
        public DbSet<Blab> Blab { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=142.93.114;database=donbstringham;user=donbstringham;password=letmein");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.LastLoginDTTM).IsRequired();
                entity.Property(e => e.RegisterDTTM).IsRequired();
            });

            modelBuilder.Entity<Blab>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DTTM).IsRequired();
                entity.Property(e => e.Message);
                entity.HasOne(u => u.User);
            });
        }
    }
}
```

- Create a ```user``` table, for example like this:

```sql
CREATE TABLE `donbstringham`.`users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `sys_id` VARCHAR(36) NOT NULL,
  `email` VARCHAR(255) NULL,
  `dttm_registration` DATETIME NULL,
  `dttm_last_login` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `Id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `sys_id_UNIQUE` (`sys_id` ASC));
```

- Create a ```blab``` table, for example like this:

```sql
CREATE TABLE `donbstringham`.`blabs` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `sys_id` VARCHAR(36) NOT NULL,
  `message` VARCHAR(255) NULL,
  `dttm_created` DATETIME NOT NULL,
  `user_id` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
  UNIQUE INDEX `sys_id_UNIQUE` (`sys_id` ASC));
```
