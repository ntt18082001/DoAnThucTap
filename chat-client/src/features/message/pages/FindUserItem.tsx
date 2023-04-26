import { Avatar, ListItem, Typography } from '@mui/material';
import { defaultAvatar, routeMessage } from '../../../constants';
import { baseURL, urlAvatar } from 'endpoints';
import React from 'react';
import { UserMessage } from 'models/messages.model';
import { useAppDispatch } from 'app/hooks';
import { setSelectedUser } from '../messageSlice';
import { useNavigate } from 'react-router-dom';

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
      <Avatar alt={props.user.name} sx={{ mr: 2 }} src={`${baseURL}/${props.user.avatar}`} />
      <Typography variant="h6" sx={{ fontWeight: '500' }}>
        {props.user.name}
      </Typography>
    </ListItem>
  );
}
export default React.memo(FindUserItem);
