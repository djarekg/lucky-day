import { Suspense } from 'react';

import Loading from '@/components/loading/loading';
import UsersLayout from '@/components/users/users-layout';

import styles from './page.module.css';

const Users = () => {
  return (
    <div className={styles.page}>
      <Suspense fallback={<Loading />}>
        <UsersLayout />;
      </Suspense>
    </div>
  );
};

export default Users;
