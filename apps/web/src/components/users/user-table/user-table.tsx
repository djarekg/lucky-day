import type { UserModel } from '@/lib/models';

import UserTableSkeleton from './user-table-skeleton';

type UserTableProps = {
  loading?: boolean;
  users: UserModel[];
};

const UserTable = ({ loading, users }: UserTableProps) => {
  if (loading) {
    return <UserTableSkeleton />;
  }

  return (
    <div>
      <h2>User Table ({users.length})</h2>
      {/* Implement the user table here */}
    </div>
  );
};

export default UserTable;
