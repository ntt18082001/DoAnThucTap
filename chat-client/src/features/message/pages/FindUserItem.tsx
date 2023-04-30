import { Avatar, ListItem, Typography } from '@mui/material';
import { routeMessage } from '../../../constants';
import { baseURL } from 'endpoints';
import React from 'react';
import { UserMessage } from 'models/messages.model';
import { useAppDispatch } from 'app/hooks';
import { setSelectedUser } from '../messageSlice';
import { useNavigate } from 'react-router-dom';
import { OfflineAvatar } from 'utils/OfflineAvatar';
import { OnlineAvatar } from 'utils/OnlineAvatar';

interface FindUserItemProps {
  user: UserMessage;
  handleHiddenPopper: () => void;
}

function FindUserItem(props: FindUserItemProps) {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleSetSelectedUser = () => {
    props.handleHiddenPopper();
    dispatch(setSelectedUser(props.user));
    navigate(`/${routeMessage}/${props.user.id}`);
  };

  return (
    <ListItem
      alignItems="center"
      sx={{
        cursor: 'pointer',
        ':hover': {
          backgroundColor: '#dbdbdb',
        },
      }}
      onClick={handleSetSelectedUser}
    >
      {!props.user.isOnline && (
        <OfflineAvatar
          overlap="circular"
          anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
          variant="dot"
        >
          <Avatar alt={props.user.name} src={`${baseURL}/${props.user.avatar}`} />
        </OfflineAvatar>
      )}
      {props.user.isOnline && (
        <OnlineAvatar
          overlap="circular"
          anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
          variant="dot"
        >
          <Avatar alt={props.user.name} src={`${baseURL}/${props.user.avatar}`} />
        </OnlineAvatar>
      )}
      <Typography variant="h6" sx={{ fontWeight: '500', ml: 2 }}>
        {props.user.name}
      </Typography>
    </ListItem>
  );
}
export default React.memo(FindUserItem);
