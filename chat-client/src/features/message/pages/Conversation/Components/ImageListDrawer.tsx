import { Box, Drawer, Typography, IconButton } from '@mui/material';
import React, { useRef, useState, useEffect } from 'react';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { GetListImg, GetListImgResponse } from 'models/messages.model';
import SkeletonImage from './SkeletonImage';
import { SlideshowLightbox } from 'lightbox.js-react';
import 'lightbox.js-react/dist/index.css';
import { baseURL } from 'endpoints';
import { useGetListMessageImageMutation } from 'features/message/message.service';
import { useAppSelector } from 'app/hooks';
import { selectUserId } from 'features/auth/authSlice';
import { selectSelectedUserId } from 'features/message/messageSlice';

type Props = {
  isOpen: boolean;
  toggleDrawer: (toggle: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => void;
};

const initialState: GetListImgResponse = {
  id: '',
  idLastMessage: undefined,
  messages: [],
  canGetMore: false,
};

function ImageListDrawer(props: Props) {
  const [firstCallApi, setFirstCallApi] = useState(true);
  const currendUserId = useAppSelector(selectUserId);
  const selectedUserId = useAppSelector(selectSelectedUserId);
  const selectConversationId = useAppSelector(
    (state) =>
      state.message.conversations.find(
        (item) =>
          (item.userId === currendUserId && item.friendId === selectedUserId) ||
          (item.userId === selectedUserId && item.friendId === currendUserId)
      )?.id
  );
  const [dataImg, setDataImg] = useState<GetListImgResponse>(initialState);
  const [allowScrollEvent, setAllowScrollEvent] = useState(true);
  const divImgRef = useRef<HTMLDivElement>(null);
  const [getListImg, { data, isSuccess, reset }] = useGetListMessageImageMutation();

  const handleGetListImg = async () => {
    try {
      const data: GetListImg = {
        id: selectConversationId,
        idLastMsg: dataImg.idLastMessage,
        lengthMessagesImg: dataImg.messages.length.toString(),
      };
      await getListImg(data).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  const handleScroll = async () => {
    if (divImgRef.current) {
      if (allowScrollEvent) {
        if (
          divImgRef.current.scrollHeight -
            (divImgRef.current.scrollTop + divImgRef.current.clientHeight) <
            100 &&
          dataImg?.canGetMore
        ) {
          setAllowScrollEvent(false);
          await handleGetListImg();
        }
      }
    }
  };

  useEffect(() => {
    if(props.isOpen && firstCallApi) {
      handleGetListImg();
      setFirstCallApi(false);
    }
  }, [firstCallApi, props.isOpen]);

  useEffect(() => {
    if (isSuccess && data) {
      setDataImg((prev) => ({
        ...data,
        messages: [...prev.messages, ...data.messages],
      }));
      reset();
      setAllowScrollEvent(true);
    }
  }, [data, isSuccess, reset]);

  useEffect(() => {
    if (!props.isOpen) {
      setDataImg(initialState);
      setFirstCallApi(true);
    }
  }, [props.isOpen]);

  return (
    <Drawer anchor="right" open={props.isOpen} onClose={props.toggleDrawer(false)}>
      <Box
        role="presentation"
        sx={{ width: '400px', p: '10px', overflowY: 'scroll' }}
        ref={divImgRef}
        onScroll={handleScroll}
      >
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <IconButton sx={{ p: '15px' }} color="secondary" onClick={props.toggleDrawer(false)}>
            <ArrowBackIcon />
          </IconButton>
          <Typography variant="h6">Danh sách hình ảnh</Typography>
        </Box>
        {dataImg.messages.length === 0 && (
          <Box sx={{ display: 'grid', gridTemplateColumns: 'auto auto auto', gap: '5px' }}>
            <SkeletonImage />
            <SkeletonImage />
            <SkeletonImage />
            <SkeletonImage />
            <SkeletonImage />
            <SkeletonImage />
          </Box>
        )}
        {dataImg.messages.length > 0 && (
          <Box>
            <SlideshowLightbox className="container grid grid-cols-3 gap-2 mx-auto grid-img">
              {dataImg.messages.map((item) => (
                <img
                  key={item.id}
                  className="w-full rounded"
                  alt={item.urlImage}
                  src={`${baseURL}/${item.urlImage}`}
                  loading="lazy"
                />
              ))}
            </SlideshowLightbox>
          </Box>
        )}
      </Box>
    </Drawer>
  );
}
export default React.memo(ImageListDrawer);
