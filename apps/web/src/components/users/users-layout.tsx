'use client';

import { useState } from 'react';

import UserCards from '@/components/users/user-cards';
import UserTable from '@/components/users/user-table';
import UsersHeader from '@/components/users/users-header';
import { type UserModel, ViewMode } from '@/lib/models';

type UsersClientProps = {
  users: UserModel[];
};

const UsersLayout = ({ users }: UsersClientProps) => {
  const [viewMode, setViewMode] = useState<ViewMode>(ViewMode.Card);

  return (
    <>
      <UsersHeader
        viewMode={viewMode}
        viewModeChange={setViewMode}
      />
      <div>
        {viewMode === ViewMode.Card ? <UserCards users={users} /> : <UserTable users={users} />}
      </div>
    </>
  );
};

export default UsersLayout;
