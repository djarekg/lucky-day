import type { UserModel } from '@/lib/models';

type UserTableProps = {
  loading?: boolean;
  users: UserModel[];
};

const UserTable = ({ loading, users }: UserTableProps) => {
  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h2>User Table</h2>
      {/* Implement the user table here */}
    </div>
  );
};

export default UserTable;
