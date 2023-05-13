import {
  Avatar,
  Box,
  Drawer,
  IconButton,
  List,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  ListSubheader,
  Typography,
} from '@mui/material';
import { baseURL } from 'endpoints';
import React, { useEffect, useRef, useState } from 'react';
import { selectSelectedUser } from '../messageSlice';
import { useAppSelector } from 'app/hooks';
import { OnlineAvatar } from 'utils/OnlineAvatar';
import { OfflineAvatar } from 'utils/OfflineAvatar';
import DeleteIcon from '@mui/icons-material/Delete';
import ImageIcon from '@mui/icons-material/Image';
import ImageListDrawer from './Conversation/Components/ImageListDrawer';
import BorderColorIcon from '@mui/icons-material/BorderColor';
import EmojiEmotionsIcon from '@mui/icons-material/EmojiEmotions';
import ColorLensIcon from '@mui/icons-material/ColorLens';
import ConfirmDialog from 'utils/ConfirmDialog';
import EmojiPicker, { EmojiClickData } from 'emoji-picker-react';
import { useUpdateInfoConvMutation, useUpdateNicknameConvMutation } from '../message.service';
import { ConversationModel, UpdateInfoConvRequest } from 'models/messages.model';
import { selectUserId } from 'features/auth/authSlice';
import ModalDialog from 'utils/ModalDialog';
import { CssTextField } from 'utils/CssTextField';
import DoneIcon from '@mui/icons-material/Done';
import { UpdateNickname } from '../../../models/messages.model';

type Props = {
  isOpen: boolean;
  toggleDrawer: (toggle: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => void;
  conv?: ConversationModel;
};

const MessageInfo = (props: Props) => {
  const currentUserId = useAppSelector(selectUserId);
  const selectedUser = useAppSelector(selectSelectedUser);
  const [drawerImg, setDrawerImg] = useState(false);
  const [openModalEmoji, setOpenModalEmoji] = React.useState(false);
  const [emojiSelect, setEmojiSelect] = React.useState('');
  useEffect(() => {
    if (props.conv?.infoConversation && props.conv?.infoConversation?.mainEmoji) {
      setEmojiSelect(props.conv?.infoConversation?.mainEmoji);
    }
  }, [props.conv?.infoConversation]);

  const [updateInfoConv, { data, isSuccess }] = useUpdateInfoConvMutation();
  const [updateNickname, updateNicknameResult] = useUpdateNicknameConvMutation();

  const { conv } = props;
  const [openModalNickname, setOpenModalNickname] = useState(false);
  const [editingIndex, setEditingIndex] = useState(-1);
  const [editingText, setEditingText] = useState('');

  const handleToggleSubmitEdit = async (nicknameId?: string, idUser?: string) => {
    try {
      if (editingText === '') {
        setEditingIndex(-1);
        return;
      }
      const data: UpdateNickname = {
        conversationId: props.conv?.id,
        nicknameId,
        nickname: editingText,
        senderId: currentUserId,
        receiverId: selectedUser?.id,
        userIdUpdated: idUser,
      };
      await updateNickname(data).unwrap();
      setEditingIndex(-1);
      setEditingText('');
    } catch (error) {
      console.log(error);
    }
  };

  const handleEnableEditing = (index: number, nickname: string) => {
    setEditingText(nickname);
    setEditingIndex(index);
  };

  const onClickSelectEmoji = (emojiData: EmojiClickData, event: MouseEvent) => {
    setEmojiSelect(emojiData.emoji);
  };

  const handleClickOpenModalEmoji = () => {
    setOpenModalEmoji(true);
  };

  const handleCloseModalEmoji = () => {
    setOpenModalEmoji(false);
  };

  const handleOpenModalNickname = () => {
    setOpenModalNickname(true);
  };

  const handleCloseModalNickname = () => {
    setOpenModalNickname(false);
  };

  const handleToggleDrawerImg =
    (open: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => {
      if (
        event.type === 'keydown' &&
        ((event as React.KeyboardEvent).key === 'Tab' ||
          (event as React.KeyboardEvent).key === 'Shift')
      ) {
        return;
      }
      setDrawerImg(open);
    };

  const handleSubmitEmoji = async () => {
    try {
      const data: UpdateInfoConvRequest = {
        conversationId: props.conv?.id,
        infoConvId:
          props.conv?.infoConversation != null ? props.conv?.infoConversation.id : undefined,
        emoji: emojiSelect,
        senderId: currentUserId,
        receiverId: selectedUser?.id,
      };
      await updateInfoConv(data).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    // Function to handle click outside of box
    const handleClickOutside = (event: MouseEvent) => {
      const box = document.getElementById("box-id"); // Replace with your box ID
      if (box && !box.contains(event.target as Node)) {
        setEditingIndex(-1); // Reset editing index
      }
    };
  
    // Add event listener when component is mounted
    document.addEventListener("mousedown", handleClickOutside);
  
    // Remove event listener when component is unmounted
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [setEditingIndex]);

  return (
    <Drawer anchor="right" open={props.isOpen} onClose={props.toggleDrawer(false)}>
      <Box role="presentation" sx={{ width: '400px' }}>
        <Box
          sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '20px' }}
        >
          {selectedUser?.isOnline && (
            <OnlineAvatar
              overlap="circular"
              anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
              variant="dot"
            >
              <Avatar
                sx={{ width: '80px', height: '80px' }}
                alt={selectedUser?.name}
                src={`${baseURL}/${selectedUser?.avatar}`}
              />
            </OnlineAvatar>
          )}
          {!selectedUser?.isOnline && (
            <OfflineAvatar
              overlap="circular"
              anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
              variant="dot"
            >
              <Avatar
                sx={{ width: '80px', height: '80px' }}
                alt={selectedUser?.name}
                src={`${baseURL}/${selectedUser?.avatar}`}
              />
            </OfflineAvatar>
          )}
          <Typography variant="h6" sx={{ fontWeight: 'bold' }}>
            {props.conv?.friendNickname ? props.conv.friendNickname.nickname : selectedUser?.name}
          </Typography>
          {selectedUser?.isOnline ? (
            <Typography className="status-connect" component="p">
              Online
            </Typography>
          ) : (
            <Typography className="status-connect" component="p">
              Offline
            </Typography>
          )}
        </Box>
        <List
          sx={{ width: '100%', bgcolor: 'background.paper' }}
          component="nav"
          aria-labelledby="nested-list-subheader"
          subheader={
            <ListSubheader component="div" id="nested-list-subheader">
              Tùy chỉnh đoạn chat
            </ListSubheader>
          }
        >
          <ListItemButton onClick={handleOpenModalNickname}>
            <ListItemIcon>
              <BorderColorIcon />
            </ListItemIcon>
            <ListItemText primary="Sửa biệt danh" />
          </ListItemButton>
          {/** Modal chỉnh sửa biệt danh */}
          <ModalDialog isOpen={openModalNickname} handleClose={handleCloseModalNickname}>
            <Typography
              sx={{
                p: 2,
                textAlign: 'center',
                borderBottom: '1px solid grey',
                fontWeight: 'bold',
                fontSize: '20px',
              }}
            >
              Sửa biệt danh
            </Typography>
            <Box sx={{ p: 2 }} id="box-id">
              <Box
                sx={{
                  p: 2,
                  display: 'flex',
                  alignItems: 'center',
                  cursor: 'pointer',
                  ':hover': {
                    backgroundColor: '#dbdbdb',
                  },
                  borderRadius: '8px',
                }}
              >
                <Avatar alt={conv?.user.name} src={`${baseURL}/${conv?.user.avatar}`} />
                <Box sx={{ width: '100%', ml: 2, mr: 1 }}>
                  {editingIndex === 1 ? (
                    <CssTextField
                      autoFocus
                      fullWidth
                      value={editingText}
                      placeholder={conv?.user.name}
                      sx={{ height: '100%' }}
                      onChange={(ev) => setEditingText(ev.target.value)}
                    />
                  ) : (
                    <>
                      <Typography sx={{ fontWeight: '600' }}>
                        {conv?.userNickname ? conv?.userNickname.nickname : conv?.user.name}
                      </Typography>
                      <Typography sx={{ fontSize: '13px' }}>
                        {conv?.userNickname ? conv?.user.name : 'Sửa biệt danh'}
                      </Typography>
                    </>
                  )}
                </Box>
                <IconButton
                  size="medium"
                  onClick={() => {
                    if (editingIndex === 1) {
                      handleToggleSubmitEdit(
                        conv?.userNickname ? conv?.userNickname.id : undefined,
                        conv?.user.id
                      );
                    } else {
                      handleEnableEditing(1, conv?.userNickname ? conv?.userNickname.nickname : '');
                    }
                  }}
                >
                  {editingIndex === 1 ? <DoneIcon /> : <BorderColorIcon />}
                </IconButton>
              </Box>
              <Box
                sx={{
                  p: 2,
                  display: 'flex',
                  alignItems: 'center',
                  cursor: 'pointer',
                  ':hover': {
                    backgroundColor: '#dbdbdb',
                  },
                  borderRadius: '8px',
                }}
              >
                <Avatar alt={conv?.friend.name} src={`${baseURL}/${conv?.friend.avatar}`} />
                <Box sx={{ width: '100%', ml: 2, mr: 1 }}>
                  {editingIndex === 2 ? (
                    <CssTextField
                      autoFocus
                      sx={{ height: '100%' }}
                      fullWidth
                      value={editingText}
                      placeholder={conv?.friend.name}
                      onChange={(ev) => setEditingText(ev.target.value)}
                    />
                  ) : (
                    <>
                      <Typography sx={{ fontWeight: '600' }}>
                        {conv?.friendNickname ? conv?.friendNickname.nickname : conv?.friend.name}
                      </Typography>
                      <Typography sx={{ fontSize: '13px' }}>
                        {conv?.friendNickname ? conv?.friend.name : 'Sửa biệt danh'}
                      </Typography>
                    </>
                  )}
                </Box>
                <IconButton
                  size="medium"
                  onClick={() => {
                    if (editingIndex === 2) {
                      handleToggleSubmitEdit(
                        conv?.friendNickname ? conv?.friendNickname.id : undefined,
                        conv?.friend.id
                      );
                    } else {
                      handleEnableEditing(
                        2,
                        conv?.friendNickname ? conv?.friendNickname.nickname : ''
                      );
                    }
                  }}
                >
                  {editingIndex === 2 ? <DoneIcon /> : <BorderColorIcon />}
                </IconButton>
              </Box>
            </Box>
          </ModalDialog>
          <ListItemButton onClick={handleClickOpenModalEmoji}>
            <ListItemIcon>
              {!props.conv?.infoConversation && !props.conv?.infoConversation?.mainEmoji && (
                <EmojiEmotionsIcon />
              )}
              {props.conv?.infoConversation && props.conv?.infoConversation?.mainEmoji && (
                <Typography
                  sx={{ fontSize: '20px' }}
                  dangerouslySetInnerHTML={{ __html: props.conv?.infoConversation?.mainEmoji }}
                />
              )}
            </ListItemIcon>
            <ListItemText primary="Thay đổi biểu cảm" />
          </ListItemButton>
          <ListItemButton>
            <ListItemIcon>
              <ColorLensIcon />
            </ListItemIcon>
            <ListItemText primary="Thay đổi màu sắc" />
          </ListItemButton>
          <ListItemButton onClick={handleToggleDrawerImg(true)}>
            <ListItemIcon>
              <ImageIcon />
            </ListItemIcon>
            <ListItemText primary="Tất cả hình ảnh" />
          </ListItemButton>
          <ImageListDrawer isOpen={drawerImg} toggleDrawer={handleToggleDrawerImg} />
          <ListItemButton sx={{ color: 'red' }}>
            <ListItemIcon sx={{ color: 'red' }}>
              <DeleteIcon />
            </ListItemIcon>
            <ListItemText primary="Xóa đoạn chat" />
          </ListItemButton>
        </List>
      </Box>
      <ConfirmDialog
        open={openModalEmoji}
        onClose={handleCloseModalEmoji}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
        handleSubmit={handleSubmitEmoji}
        titleSubmit="Đồng ý"
      >
        <Box sx={{ p: 2 }}>
          <Typography>
            Emoji đã chọn:{' '}
            <Typography
              component="span"
              sx={{ fontSize: '25px' }}
              dangerouslySetInnerHTML={{ __html: emojiSelect }}
            />
          </Typography>
        </Box>
        <EmojiPicker onEmojiClick={onClickSelectEmoji} lazyLoadEmojis />
      </ConfirmDialog>
    </Drawer>
  );
};

export default React.memo(MessageInfo);
