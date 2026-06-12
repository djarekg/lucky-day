import { Button, Card, CardActions, CardContent, Typography } from '@mui/material';

import type { UserModel } from '@/lib/models';

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

const UserCards = ({ users }: { users: UserModel[] }) => {
  return (
    <div>
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
