import { makeStyles } from '@griffel/react';
import { Button, Card, CardActions, CardContent, Typography } from '@mui/material';

import type { UserModel } from '@/lib/models';

type UserCardsProps = {
  loading?: boolean;
  users: UserModel[];
};

const useStyles = makeStyles({
  container: {
    display: 'grid',
    gridTemplateColumns: 'repeat(auto-fill, minmax(240px, 1fr))',
    gap: '1rem',
    alignItems: 'stretch',
  },
});

const UserCard = (user: UserModel) => {
  return (
    <Card
      key={user.id}
      sx={{ maxWidth: 345, marginBottom: 2 }}>
      <CardContent>
        <Typography
          gutterBottom
          sx={{ fontSize: 14 }}
          color="textSecondary"
          component="div">
          {user.firstName} {user.lastName}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions>
    </Card>
  );
};

const UserCards = ({ loading, users }: UserCardsProps) => {
  if (loading) {
    return <div>Loading...</div>;
  }

  const styles = useStyles();

  return (
    <div className={styles.container}>
      {users.map(user => (
        <UserCard
          key={user.id}
          {...user}
        />
      ))}
    </div>
  );
};

export default UserCards;
