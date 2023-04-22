import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  CardMedia,
  Grid,
  IconButton,
  Tab,
  Tabs,
  Typography,
} from '@mui/material';
import { useAppDispatch, useAppSelector } from 'app/hooks';
import { baseURL } from 'endpoints';
import { selectUser, setUser } from 'features/auth/authSlice';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import useMode from 'hooks/useMode';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import FormUser from './FormUser';
import { UserModel } from 'models/useridentity.model';
import { useUpdateUserMutation } from 'features/auth/auth.service';
import { toast } from 'react-toastify';
import PhotoCameraIcon from '@mui/icons-material/PhotoCamera';
import ModalUpdateAvatar from './ModalUpdateAvatar';
import FormChangePassword from './FormChangePassword';
import KeyIcon from '@mui/icons-material/Key';
import EditIcon from '@mui/icons-material/Edit';
import ListFriend from './ListFriend';

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}

function a11yProps(index: number) {
  return {
    id: `simple-tab-${index}`,
    'aria-controls': `simple-tabpanel-${index}`,
  };
}

function Profile() {
  const { id } = useParams();

  const dispatch = useAppDispatch();

  const isDarkmode = useAppSelector(selectIsDarkmode);
  const user = useAppSelector(selectUser);

  const darkmode = useMode(isDarkmode);

  const [updateUser, { data, isLoading, isSuccess }] = useUpdateUserMutation();

  const [value, setValue] = useState(0);
  const [openModal, setOpenModal] = useState(false);
  const [openModalPwd, setOpenModalPwd] = useState(false);

  const handleClickOpenModal = () => {
    setOpenModal(true);
  };

  const handleCloseModal = () => {
    setOpenModal(false);
  };

  const handleClickOpenModalPwd = () => {
    setOpenModalPwd(true);
  };

  const handleCloseModalPwd = () => {
    setOpenModalPwd(false);
  };

  useEffect(() => {
    if (isSuccess) {
      if (data) {
        dispatch(setUser(data));
      }
      toast.success('Cập nhập thành công!');
    }
  }, [dispatch, data, isSuccess]);

  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  const handleSubmitUser = async (values: UserModel) => {
    await updateUser(values).unwrap();
  };

  return (
    <>
      <Grid container>
        <Grid item xs={6} md={4} sm={12} sx={{ mt: 10 }}>
          <Card
            sx={{
              maxWidth: 345,
              bgcolor: darkmode?.bgColor,
              color: darkmode?.color,
            }}
          >
            <CardMedia
              sx={{
                height: 300,
                display: 'flex',
                justifyContent: 'flex-end',
                alignItems: 'flex-end',
              }}
              image={`${baseURL}/${user?.avatar}`}
              title={user?.fullName}
            >
              <IconButton aria-label="delete" size="large" onClick={handleClickOpenModal}>
                <PhotoCameraIcon fontSize="inherit" />
              </IconButton>
            </CardMedia>
            <CardContent>
              <Typography gutterBottom variant="h5" component="div" textAlign="center">
                {user?.fullName}
              </Typography>
              <Typography component="div">
                Mật khẩu:    ********
                <IconButton aria-label="Đổi mật khẩu" color='primary' sx={{ marginLeft: 2 }} onClick={handleClickOpenModalPwd}>
                  <KeyIcon />
                </IconButton>
              </Typography>
            </CardContent>
          </Card>
        </Grid>
        <Grid item xs={6} md={8} sm={12} sx={{ mt: 10 }}>
          <Box
            sx={{
              width: '100%',
              border: darkmode?.borderColor,
              backgroundColor: darkmode?.bgColor,
              borderRadius: 2,
            }}
          >
            <Box sx={{ borderBottom: 1, borderColor: 'divider', color: darkmode?.color }}>
              <Tabs
                value={value}
                onChange={handleChange}
                textColor="inherit"
                indicatorColor="secondary"
              >
                <Tab label="Thông tin cá nhân" {...a11yProps(0)} />
                <Tab label="Bạn bè" {...a11yProps(1)} />
              </Tabs>
            </Box>
            <TabPanel value={value} index={0}>
              {user && <FormUser model={user} onSubmit={handleSubmitUser} isLoading={isLoading} />}
            </TabPanel>
            <TabPanel value={value} index={1}>
              <ListFriend />
            </TabPanel>
          </Box>
        </Grid>
      </Grid>
      <ModalUpdateAvatar
        handleClose={handleCloseModal}
        isOpen={openModal}
        urlAvatar={user?.avatar}
        id={user?.id}
      />
      <FormChangePassword
        handleClose={handleCloseModalPwd}
        isOpen={openModalPwd}
        id={user?.id}
      />
    </>
  );
}

export default React.memo(Profile);
