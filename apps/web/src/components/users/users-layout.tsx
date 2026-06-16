'use client';

import { useEffect, useState, useTransition } from 'react';

import UsersHeader from '@/components/users/users-header';
import { fetchUsers } from '@/lib/actions/user.actions';
import { type UserModel, ViewMode } from '@/lib/models';

import UserCards from './user-cards/user-cards';
import UserTable from './user-table/user-table';

const UsersLayout = () => {
  const [users, setUsers] = useState<UserModel[] | null>(null);
  const [viewMode, setViewMode] = useState<ViewMode>(ViewMode.Card);
  const [loading, startTransition] = useTransition();

  const reload = () => {
    startTransition(async () => {
      setUsers(await fetchUsers());
    });
  };

  // Load users on initial mount.
  useEffect(() => {
    reload();
  }, []);

  const isLoading = loading || users === null;

  return (
    <>
      <UsersHeader
        viewMode={viewMode}
        viewModeChange={setViewMode}
        onReload={reload}
      />
      <div>
        {viewMode === ViewMode.Card ? (
          <UserCards
            loading={isLoading}
            users={users || []}
          />
        ) : (
          <UserTable
            loading={isLoading}
            users={users || []}
          />
        )}
      </div>
    </>
  );
};

export default UsersLayout;
