import SendIcon from '@mui/icons-material/Send';
import { Box, Grid, IconButton, Paper, Typography } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import React, { ChangeEvent, useState } from 'react';
import { borderColorDarkmode, borderColorDefault } from '../../../../constants';
import { CssInputBase } from '../../../../utils/CssTextField';
import { PhotoCamera } from '@mui/icons-material';
import EmojiEmotionsIcon from '@mui/icons-material/EmojiEmotions';
import EmojiPicker, { EmojiClickData } from 'emoji-picker-react';

interface Props {
  onSubmit: (message: string, file?: File) => void;
  mainEmoji?: string;
}

const ConversationFormMessage = (props: Props) => {
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const [message, setMessage] = useState<string>('');
  const [isShowEmojiPicker, setIsShowEmojiPicker] = useState(false);

  const borderColor = isDarkmode ? borderColorDarkmode : borderColorDefault;

  const toggleShowPicker = () => {
    setIsShowEmojiPicker(!isShowEmojiPicker);
  };

  const onClickSelectEmoji = (emojiData: EmojiClickData, event: MouseEvent) => {
    setMessage(message + emojiData.emoji);
  };

  const handleChangeFile = (ev: ChangeEvent<HTMLInputElement>) => {
    const selectedFile = ev.target.files?.[0];
    if (selectedFile) {
      props.onSubmit(message, selectedFile);
    }
  };

  return (
    <Grid
      item
      sx={{ borderTop: borderColor, maxHeight: '51px', height: '51px' }}
      display="flex"
      alignItems="flex-end"
    >
      <Paper
        component="form"
        sx={{
          display: 'flex',
          alignItems: 'center',
          bgcolor: 'transparent',
          width: '100%',
          height: '100%',
        }}
        onSubmit={(ev) => {
          ev.preventDefault();
          props.onSubmit(message.trimStart());
          setMessage('');
        }}
      >
        <IconButton sx={{ p: '15px' }} color="secondary" component="label">
          <input hidden accept="image/*" multiple type="file" onChange={handleChangeFile} />
          <PhotoCamera />
        </IconButton>
        <CssInputBase
          sx={{
            pl: 2,
            width: '100%',
            height: '100%',
            borderRight: borderColor,
            borderLeft: borderColor,
          }}
          placeholder="Aa"
          multiline
          maxRows={3}
          onChange={(ev) => setMessage(ev.target.value)}
          onKeyDown={(ev: React.KeyboardEvent<HTMLTextAreaElement>) => {
            if (ev.key === 'Enter' && !ev.shiftKey) {
              ev.preventDefault();
              props.onSubmit(message.trimStart());
              setMessage('');
            }
          }}
          value={message}
        />
        {isShowEmojiPicker && (
          <Box className="emoji-position">
            <EmojiPicker onEmojiClick={onClickSelectEmoji} lazyLoadEmojis />
          </Box>
        )}
        <Box sx={{ borderRight: borderColor }}>
          <IconButton sx={{ p: '15px' }} color="warning" onClick={toggleShowPicker}>
            <EmojiEmotionsIcon />
          </IconButton>
        </Box>
        <IconButton sx={{ p: '15px' }} color="secondary" type="submit">
          {!props.mainEmoji && <SendIcon />}
          {props.mainEmoji && (
            <Typography
              sx={{ fontSize: '20px' }}
              dangerouslySetInnerHTML={{ __html: props.mainEmoji }}
            />
          )}
        </IconButton>
      </Paper>
    </Grid>
  );
};

export default ConversationFormMessage;
