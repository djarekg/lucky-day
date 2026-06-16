import { makeStyles } from '@griffel/react';
import { Avatar, Button, Card, CardActions, CardContent, CardHeader } from '@mui/material';

import type { UserModel } from '@/lib/models';

import UserCardSkeleton from './user-card-skeleton';

type UserCardsProps = {
  loading?: boolean;
  users: UserModel[];
};

const SKELETON_COUNT = 42;
const CARD_MAX_WIDTH = 375;

const useStyles = makeStyles({
  container: {
    display: 'grid',
    gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))',
    gap: '1rem',
    alignItems: 'stretch',
  },
});

const UserCard = ({ user }: { user: UserModel }) => {
  return (
    <Card
      key={user.id}
      sx={{ maxWidth: CARD_MAX_WIDTH, marginBottom: 2 }}>
      <CardHeader
        avatar={
          <Avatar
            alt={`${user.firstName} ${user.lastName}`}
            src={`/avatar.svg`}
          />
        }
        title={`${user.firstName} ${user.lastName}`}
        subheader={user.jobTitle}
      />
      <CardContent></CardContent>
      <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions>
    </Card>
  );
};

const UserCards = ({ loading, users }: UserCardsProps) => {
  const styles = useStyles();

  return (
    <div className={styles.container}>
      {loading
        ? Array.from({ length: SKELETON_COUNT }).map((_, index) => <UserCardSkeleton key={index} />)
        : users.map(user => (
            <UserCard
              key={user.id}
              user={user}
            />
          ))}
    </div>
  );
};

export default UserCards;
