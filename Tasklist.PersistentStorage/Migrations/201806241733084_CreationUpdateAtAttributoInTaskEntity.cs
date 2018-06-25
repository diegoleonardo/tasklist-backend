namespace Tasklist.PersistentStorage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreationUpdateAtAttributoInTaskEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "InProgressAt", c => c.DateTime());
            AlterColumn("dbo.Tasks", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Tasks", "DeletedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "DeletedAt", c => c.DateTime());
            AlterColumn("dbo.Tasks", "Title", c => c.String());
            DropColumn("dbo.Tasks", "InProgressAt");
        }
    }
}
