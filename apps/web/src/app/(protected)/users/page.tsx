import { getUsers } from '@/app/api/user.api';
import UsersLayout from '@/components/users/users-layout';

import styles from './page.module.css';

const Users = async () => {
  const users = await getUsers();

  return (
    <div className={styles.page}>
      <UsersLayout users={users} />
    </div>
  );
};

export default Users;
